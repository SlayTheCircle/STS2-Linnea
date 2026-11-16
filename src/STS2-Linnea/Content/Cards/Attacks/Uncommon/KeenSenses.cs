namespace LinneaMod.Content.Cards;

/// <summary>敏锐感官：按原案与升级表实现。</summary>
[RegisterCard(typeof(LinneaCardPool))]
public sealed class KeenSenses : LinneaCardBase
{
    protected override IEnumerable<DynamicVar> CanonicalVars => new DynamicVar[] { new DamageVar(10m, ValueProp.Move), new BlockVar(6m, ValueProp.Move), new DynamicVar("NextBlock", 7m) };
    public override bool GainsBlock => true;

    public KeenSenses() : base(2, CardType.Attack, CardRarity.Uncommon, TargetType.AnyEnemy) { }
    protected override async Task OnPlay(PlayerChoiceContext context, CardPlay play)
    {
        ArgumentNullException.ThrowIfNull(play.Target);
        await DamageCmd.Attack(DynamicVars.Damage.BaseValue).FromCard(this, play).Targeting(play.Target!).WithHitFx("vfx/vfx_attack_slash").Execute(context);
        await CreatureCmd.GainBlock(Owner.Creature, DynamicVars.Block, play);
        await PowerCmd.Apply<LinneaMod.Content.Powers.BlockNextTurnPower>(context, Owner.Creature, DynamicVars["NextBlock"].BaseValue, Owner.Creature, this);
    }
    protected override void OnUpgrade()
    {
        DynamicVars["Damage"].UpgradeValueBy(1m);
        DynamicVars["Block"].UpgradeValueBy(2m);
        DynamicVars["NextBlock"].UpgradeValueBy(2m);
    }
}
