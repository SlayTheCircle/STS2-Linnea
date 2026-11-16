namespace LinneaMod.Content.Cards;

/// <summary>漫游世界：按原案与升级表实现。</summary>
[RegisterCard(typeof(LinneaCardPool))]
public sealed class WorldRoaming : LinneaCardBase
{
    protected override IEnumerable<IHoverTip> AdditionalHoverTips => new IHoverTip[] { HoverTipFactory.FromCard<Lumi>() };
    protected override IEnumerable<DynamicVar> CanonicalVars => new DynamicVar[] { new DynamicVar("Block", 5m) };
    public override CardMultiplayerConstraint MultiplayerConstraint => CardMultiplayerConstraint.MultiplayerOnly;
    public WorldRoaming() : base(2, CardType.Power, CardRarity.Rare, TargetType.Self) { }
    protected override async Task OnPlay(PlayerChoiceContext context, CardPlay play)
    {
        await PowerCmd.Apply<WorldRoamingPower>(context, Owner.Creature, DynamicVars["Block"].BaseValue, Owner.Creature, this);
    }
    protected override void OnUpgrade()
    {
        DynamicVars["Block"].UpgradeValueBy(2m);
        EnergyCost.UpgradeBy(-1);
    }
}
