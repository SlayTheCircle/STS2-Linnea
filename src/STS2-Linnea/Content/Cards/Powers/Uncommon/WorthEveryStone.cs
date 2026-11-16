namespace LinneaMod.Content.Cards;

/// <summary>物有所值：按原案与升级表实现。</summary>
[RegisterCard(typeof(LinneaCardPool))]
public sealed class WorthEveryStone : LinneaCardBase
{
    protected override IEnumerable<DynamicVar> CanonicalVars => new DynamicVar[] { new DynamicVar("Damage", 3m) };
    protected override IEnumerable<IHoverTip> AdditionalHoverTips => new[] { HoverTipFactory.FromPower<WorthEveryStonePower>(DynamicVars["Damage"].IntValue) };
    public WorthEveryStone() : base(2, CardType.Power, CardRarity.Uncommon, TargetType.Self) { }
    protected override async Task OnPlay(PlayerChoiceContext context, CardPlay play)
    {
        await PowerCmd.Apply<WorthEveryStonePower>(context, Owner.Creature, DynamicVars["Damage"].BaseValue, Owner.Creature, this);
    }
    protected override void OnUpgrade()
    {
        EnergyCost.UpgradeBy(-1);
        DynamicVars["Damage"].UpgradeValueBy(1m);
    }
}
