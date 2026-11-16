namespace LinneaMod.Content.Relics;

[RegisterRelic(typeof(LinneaRelicPool))]
public sealed class SuperLumi : LinneaRelicBase
{
    public override RelicRarity Rarity => RelicRarity.Event;

    protected override IEnumerable<IHoverTip> AdditionalHoverTips => new[] { HoverTipFactory.FromCard<Lumi>() };
    public override Task AfterCardGeneratedForCombat(CardModel card, Player? creator)
    {
        if (card.Owner == Owner && card is Lumi && card.IsUpgradable) { Flash(); CardCmd.Upgrade(card); }
        return Task.CompletedTask;
    }
}
