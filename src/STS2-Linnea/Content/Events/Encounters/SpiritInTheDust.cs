using MegaCrit.Sts2.Core.Events;
using MegaCrit.Sts2.Core.Models.Acts;
using STS2RitsuLib.Scaffolding.Content;

namespace LinneaMod.Content.Events;

[RegisterActEvent(typeof(Glory))]
public sealed class SpiritInTheDust : LinneaEventBase
{
    public override EventAssetProfile AssetProfile => new(
        InitialPortraitPath: "res://STS2-Linnea/images/events/SpiritInTheDust.png");
    protected override IEnumerable<DynamicVar> CanonicalVars => new DynamicVar[] { new GoldVar("Price", 200), new DynamicVar("MaxHp", 8) };
    protected override IReadOnlyList<EventOption> GenerateInitialOptions()
    {

        bool curse = Owner!.Deck.Cards.Any(card => card.Type == CardType.Curse && card.IsRemovable);
        bool gold = Owner.Gold >= DynamicVars["Price"].IntValue;
        string offering = !curse ? "OFFER_NO_CURSE" : !gold ? "OFFER_NO_GOLD" : "OFFER";
        return new[] {
            new EventOption(this, curse && gold ? Offer : null, InitialOptionKey(offering)),
            new EventOption(this, Accept, InitialOptionKey("ACCEPT")) };

    }

    private async Task Offer()
    {
        if (Owner!.Gold < DynamicVars["Price"].IntValue) { SetEventState(InitialDescription, GenerateInitialOptions()); return; }
        var selected = (await CardSelectCmd.FromDeckForRemoval(Owner,
            new CardSelectorPrefs(CardSelectorPrefs.RemoveSelectionPrompt, 1), card => card.Type == CardType.Curse)).ToList();
        if (selected.Count == 0) { SetEventState(InitialDescription, GenerateInitialOptions()); return; }
        await PlayerCmd.LoseGold(DynamicVars["Price"].BaseValue, Owner, MegaCrit.Sts2.Core.Entities.Gold.GoldLossType.Spent);
        await CardPileCmd.RemoveFromDeck(selected);
        SetEventFinished(PageDescription("OFFER"));
    }
    private async Task Accept()
    {
        await CreatureCmd.GainMaxHp(Owner!.Creature, DynamicVars["MaxHp"].BaseValue);
        SetEventFinished(PageDescription("ACCEPT"));
    }

}
