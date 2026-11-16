using MegaCrit.Sts2.Core.Events;
using MegaCrit.Sts2.Core.Models.Acts;
using STS2RitsuLib.Scaffolding.Content;

namespace LinneaMod.Content.Events;

[RegisterActEvent(typeof(Overgrowth))]
[RegisterActEvent(typeof(Underdocks))]
public sealed class JungleDiscovery : LinneaEventBase
{
    public override EventAssetProfile AssetProfile => new(
        InitialPortraitPath: "res://STS2-Linnea/images/events/JungleDiscovery.png");
    protected override IEnumerable<DynamicVar> CanonicalVars => new DynamicVar[] { new GoldVar("Gold", 300), new DynamicVar("Upgrades", 1) };
    protected override IReadOnlyList<EventOption> GenerateInitialOptions()
    {

        return new[] {
            new EventOption(this, TakeGold, InitialOptionKey("GOLD")),
            new EventOption(this, RecordPlants, InitialOptionKey("PLANTS")),
            new EventOption(this, Owner!.Relics.Any(r => r is SuperLumi) ? null : FeedOre,
                InitialOptionKey(Owner.Relics.Any(r => r is SuperLumi) ? "ORE_LOCKED" : "ORE"), HoverTipFactory.FromRelic<SuperLumi>()) };

    }

    private async Task TakeGold()
    {
        await PlayerCmd.GainGold(DynamicVars.Gold.BaseValue, Owner!);
        SetEventFinished(PageDescription("GOLD"));
    }
    private async Task RecordPlants()
    {
        foreach (var card in await CardSelectCmd.FromDeckForUpgrade(Owner!,
            new CardSelectorPrefs(CardSelectorPrefs.UpgradeSelectionPrompt, DynamicVars["Upgrades"].IntValue))) CardCmd.Upgrade(card);
        SetEventFinished(PageDescription("PLANTS"));
    }
    private async Task FeedOre()
    {
        await RelicCmd.Obtain<SuperLumi>(Owner!);
        SetEventFinished(PageDescription("ORE"));
    }

}
