namespace LinneaMod.Content.Cards;

/// <summary>收集图鉴：按原案与升级表实现。</summary>
[RegisterCard(typeof(LinneaCardPool))]
public sealed class CollectBestiary : LinneaCardBase
{

    protected override IEnumerable<DynamicVar> CanonicalVars => new DynamicVar[] {
        new CalculationBaseVar(3), new ExtraDamageVar(3),
        new CalculatedDamageVar(ValueProp.Move).WithMultiplier((card, _) => LinneaRunRecord.Count(card.Owner)) };

    public CollectBestiary() : base(1, CardType.Attack, CardRarity.Rare, TargetType.AnyEnemy) { }
    protected override async Task OnPlay(PlayerChoiceContext context, CardPlay play)
    {
        ArgumentNullException.ThrowIfNull(play.Target);
        await DamageCmd.Attack(DynamicVars.CalculatedDamage).FromCard(this, play).Targeting(play.Target!).WithHitFx("vfx/vfx_attack_slash").Execute(context);
    }
    protected override void OnUpgrade()
    {
        DynamicVars.CalculationBase.UpgradeValueBy(2); DynamicVars.ExtraDamage.UpgradeValueBy(1);
    }
}
