using MegaCrit.Sts2.Core.Events;
using MegaCrit.Sts2.Core.Models.Acts;
using STS2RitsuLib.Scaffolding.Content;

namespace LinneaMod.Content.Events;

[RegisterActEvent(typeof(Hive))]
public sealed class OreCollector : LinneaEventBase
{
    public override EventAssetProfile AssetProfile => new(
        InitialPortraitPath: "res://STS2-Linnea/images/events/OreCollector.png");
    protected override IEnumerable<DynamicVar> CanonicalVars => new DynamicVar[] { new GoldVar("Price", 175), new DynamicVar("Upgrades", 2) };
    protected override IReadOnlyList<EventOption> GenerateInitialOptions()
    {

        bool gift = Owner!.Deck.Cards.Any(c => c.IsUpgraded && c.IsRemovable);
        bool buy = Owner.Gold >= DynamicVars["Price"].IntValue;
        return new[] {
            new EventOption(this, Record, InitialOptionKey("RECORD")),
            new EventOption(this, gift ? Give : null, InitialOptionKey(gift ? "GIVE" : "GIVE_LOCKED")),
            new EventOption(this, buy ? Buy : null, InitialOptionKey(buy ? "BUY" : "BUY_LOCKED")) };

    }

    private Task Record()
    {
        EventDeckChanges.UpgradeRandom(Owner!, Rng, DynamicVars["Upgrades"].IntValue);
        SetEventFinished(PageDescription("RECORD"));
        return Task.CompletedTask;
    }
    private async Task Give()
    {
        var selected = (await CardSelectCmd.FromDeckForRemoval(Owner!,
            new CardSelectorPrefs(CardSelectorPrefs.RemoveSelectionPrompt, 1), card => card.IsUpgraded)).ToList();
        if (selected.Count == 0) { SetEventState(InitialDescription, GenerateInitialOptions()); return; }
        await CardPileCmd.RemoveFromDeck(selected);
        SetEventFinished(PageDescription("GIVE"));
    }
    private async Task Buy()
    {
        if (Owner!.Gold < DynamicVars["Price"].IntValue) { SetEventState(InitialDescription, GenerateInitialOptions()); return; }
        await PlayerCmd.LoseGold(DynamicVars["Price"].BaseValue, Owner, MegaCrit.Sts2.Core.Entities.Gold.GoldLossType.Spent);
        await RelicCmd.Obtain(MegaCrit.Sts2.Core.Factories.RelicFactory.PullNextRelicFromFront(Owner, Rng).ToMutable(), Owner);
        SetEventFinished(PageDescription("BUY"));
    }

}
