namespace LinneaMod.Content.Cards;

/// <summary>精准打击：按原案与升级表实现。</summary>
[RegisterCard(typeof(LinneaCardPool))]
public sealed class PreciseStrike : LinneaCardBase
{
    protected override IEnumerable<IHoverTip> AdditionalHoverTips => new IHoverTip[] { HoverTipFactory.FromPower<VulnerablePower>() };
    protected override IEnumerable<DynamicVar> CanonicalVars => new DynamicVar[] { new DamageVar(3m, ValueProp.Move), new DynamicVar("Vulnerable", 1m) };
    protected override HashSet<CardTag> CanonicalTags => new() { CardTag.Strike };

    public PreciseStrike() : base(0, CardType.Attack, CardRarity.Common, TargetType.AnyEnemy) { }
    protected override async Task OnPlay(PlayerChoiceContext context, CardPlay play)
    {
        ArgumentNullException.ThrowIfNull(play.Target);
        await DamageCmd.Attack(DynamicVars.Damage.BaseValue).FromCard(this, play).Targeting(play.Target!).WithHitFx("vfx/vfx_attack_slash").Execute(context);
        await PowerCmd.Apply<VulnerablePower>(context, play.Target!, DynamicVars["Vulnerable"].BaseValue, Owner.Creature, this);
    }
    protected override void OnUpgrade()
    {
        DynamicVars["Damage"].UpgradeValueBy(1m);
        DynamicVars["Vulnerable"].UpgradeValueBy(1m);
    }
}
