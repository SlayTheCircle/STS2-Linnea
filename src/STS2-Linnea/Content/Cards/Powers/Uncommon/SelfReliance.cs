namespace LinneaMod.Content.Cards;

/// <summary>自力更生：按原案与升级表实现。</summary>
[RegisterCard(typeof(LinneaCardPool))]
public sealed class SelfReliance : LinneaCardBase
{
    protected override IEnumerable<IHoverTip> AdditionalHoverTips => new IHoverTip[] { HoverTipFactory.FromCard<Lumi>() };
    protected override IEnumerable<DynamicVar> CanonicalVars => new DynamicVar[] { new DynamicVar("Effect", 8m) };

    public SelfReliance() : base(1, CardType.Power, CardRarity.Uncommon, TargetType.Self) { }
    protected override async Task OnPlay(PlayerChoiceContext context, CardPlay play)
    {
        await PowerCmd.Apply<SelfReliancePower>(context, Owner.Creature, DynamicVars["Effect"].BaseValue, Owner.Creature, this);
    }
    protected override void OnUpgrade()
    {
        DynamicVars["Effect"].UpgradeValueBy(2m);
    }
}
