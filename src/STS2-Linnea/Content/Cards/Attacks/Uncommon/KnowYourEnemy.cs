namespace LinneaMod.Content.Cards;

/// <summary>知己知彼：按原案与升级表实现。</summary>
[RegisterCard(typeof(LinneaCardPool))]
public sealed class KnowYourEnemy : LinneaCardBase
{
    protected override IEnumerable<DynamicVar> CanonicalVars => new DynamicVar[] { new DamageVar(3m, ValueProp.Move) };

    public KnowYourEnemy() : base(1, CardType.Attack, CardRarity.Uncommon, TargetType.AnyEnemy) { }
    protected override async Task OnPlay(PlayerChoiceContext context, CardPlay play)
    {
        ArgumentNullException.ThrowIfNull(play.Target);
        await DamageCmd.Attack(DynamicVars.Damage.BaseValue).FromCard(this, play).Targeting(play.Target!).WithHitFx("vfx/vfx_attack_slash").Execute(context);
        await PowerCmd.Apply<KnowYourEnemyPower>(context, Owner.Creature, 1, Owner.Creature, this);
    }
    protected override void OnUpgrade()
    {
        DynamicVars["Damage"].UpgradeValueBy(2m);
    }
}
