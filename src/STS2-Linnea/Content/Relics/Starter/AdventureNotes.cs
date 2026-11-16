namespace LinneaMod.Content.Relics;

[RegisterRelic(typeof(LinneaRelicPool))]
[RegisterTouchOfOrobasRefinement(typeof(ConchGangLegacy))]
public sealed class AdventureNotes : LinneaRelicBase
{
    protected override IEnumerable<IHoverTip> AdditionalHoverTips => new[] { HoverTipFactory.FromCard<Lumi>() };
    public override RelicRarity Rarity => RelicRarity.Starter;

    public override async Task BeforeCombatStart()
        => await LumiCompanion.Generate(new ThrowingPlayerChoiceContext(), Owner);

}
