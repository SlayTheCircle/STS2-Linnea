namespace LinneaMod.Content.Cards;

/// <summary>两面夹击：按原案与升级表实现。</summary>
[RegisterCard(typeof(LinneaCardPool))]
public sealed class PincerAttack : LinneaCardBase
{
    protected override IEnumerable<IHoverTip> AdditionalHoverTips => new IHoverTip[] { HoverTipFactory.FromCard<Lumi>() };
    protected override IEnumerable<DynamicVar> CanonicalVars => new DynamicVar[] { new DamageVar(11m, ValueProp.Move) };

    public PincerAttack() : base(2, CardType.Attack, CardRarity.Uncommon, TargetType.AnyEnemy) { }
    protected override async Task OnPlay(PlayerChoiceContext context, CardPlay play)
    {
        ArgumentNullException.ThrowIfNull(play.Target);
        await DamageCmd.Attack(DynamicVars.Damage.BaseValue).FromCard(this, play).Targeting(play.Target!).WithHitFx("vfx/vfx_attack_slash").Execute(context);
        await LumiCompanion.Play(context, Owner, play.Target);
    }
    protected override void OnUpgrade()
    {
        DynamicVars["Damage"].UpgradeValueBy(5m);
    }
}
