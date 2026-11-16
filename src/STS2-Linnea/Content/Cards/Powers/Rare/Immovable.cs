namespace LinneaMod.Content.Cards;

/// <summary>不动如山：按原案与升级表实现。</summary>
[RegisterCard(typeof(LinneaCardPool))]
public sealed class Immovable : LinneaCardBase
{
    protected override IEnumerable<DynamicVar> CanonicalVars => new DynamicVar[] { new DynamicVar("Block", 12m) };
    protected override IEnumerable<IHoverTip> AdditionalHoverTips => new[] { HoverTipFactory.FromPower<IntangiblePower>(1), HoverTipFactory.FromPower<ImmovablePower>(DynamicVars["Block"].IntValue) };
    public Immovable() : base(3, CardType.Power, CardRarity.Rare, TargetType.Self) { }
    protected override async Task OnPlay(PlayerChoiceContext context, CardPlay play)
    {
        await PowerCmd.Apply<IntangiblePower>(context, Owner.Creature, 1, Owner.Creature, this);
        await PowerCmd.Apply<ImmovablePower>(context, Owner.Creature, DynamicVars["Block"].BaseValue, Owner.Creature, this);
    }
    protected override void OnUpgrade()
    {
        DynamicVars["Block"].UpgradeValueBy(6m);
    }
}
