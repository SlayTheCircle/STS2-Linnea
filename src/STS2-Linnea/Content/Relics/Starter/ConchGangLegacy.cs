namespace LinneaMod.Content.Relics;

[RegisterRelic(typeof(LinneaRelicPool))]
public sealed class ConchGangLegacy : LinneaRelicBase
{
    protected override IEnumerable<IHoverTip> AdditionalHoverTips => new[] { HoverTipFactory.FromCard<Lumi>() };
    protected override IEnumerable<string> RegisteredKeywordIds => new[] { LinneaKeywords.Encourage };
    public override RelicRarity Rarity => RelicRarity.Ancient;

    public override async Task BeforeCombatStart()
    {
        Lumi? lumi = await LumiCompanion.Generate(new ThrowingPlayerChoiceContext(), Owner);
        if (lumi is not null && lumi.IsUpgradable) CardCmd.Upgrade(lumi);
    }

    public override async Task AfterPlayerTurnStart(PlayerChoiceContext context, Player player)
    {
        if (player == Owner) await LumiCompanion.Encourage(context, Owner, 4, null);
    }

}
