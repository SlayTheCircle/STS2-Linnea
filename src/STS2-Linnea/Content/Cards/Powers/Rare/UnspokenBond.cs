namespace LinneaMod.Content.Cards;

/// <summary>默契无间：按原案与升级表实现。</summary>
[RegisterCard(typeof(LinneaCardPool))]
public sealed class UnspokenBond : LinneaCardBase
{
    protected override IEnumerable<IHoverTip> AdditionalHoverTips => new IHoverTip[] { HoverTipFactory.FromCard<Lumi>() };
    protected override IEnumerable<DynamicVar> CanonicalVars => new DynamicVar[] { new DynamicVar("Effect", 1m) };

    public UnspokenBond() : base(2, CardType.Power, CardRarity.Rare, TargetType.Self) { }
    protected override async Task OnPlay(PlayerChoiceContext context, CardPlay play)
    {
        await PowerCmd.Apply<UnspokenBondPower>(context, Owner.Creature, DynamicVars["Effect"].BaseValue, Owner.Creature, this);
    }
    protected override void OnUpgrade()
    {
        EnergyCost.UpgradeBy(-1);
    }
}
