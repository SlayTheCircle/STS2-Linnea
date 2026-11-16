namespace LinneaMod.Content.Cards;

/// <summary>大胆推测：按原案与升级表实现。</summary>
[RegisterCard(typeof(LinneaCardPool))]
public sealed class BoldSpeculation : LinneaCardBase
{
    protected override IEnumerable<IHoverTip> AdditionalHoverTips => new IHoverTip[] { HoverTipFactory.FromPower<WeakPower>() };
    protected override IEnumerable<DynamicVar> CanonicalVars => new DynamicVar[] { new DamageVar(4m, ValueProp.Move), new DynamicVar("NextDamage", 6m) };

    public BoldSpeculation() : base(0, CardType.Attack, CardRarity.Uncommon, TargetType.AnyEnemy) { }
    protected override async Task OnPlay(PlayerChoiceContext context, CardPlay play)
    {
        ArgumentNullException.ThrowIfNull(play.Target);
        await DamageCmd.Attack(DynamicVars.Damage.BaseValue).FromCard(this, play).Targeting(play.Target!).WithHitFx("vfx/vfx_attack_slash").Execute(context);
        await PowerCmd.Apply<BoldSpeculationPower>(context, Owner.Creature, DynamicVars["NextDamage"].BaseValue, Owner.Creature, this);
    }
    protected override void OnUpgrade()
    {
        DynamicVars["Damage"].UpgradeValueBy(1m);
        DynamicVars["NextDamage"].UpgradeValueBy(2m);
    }
}
