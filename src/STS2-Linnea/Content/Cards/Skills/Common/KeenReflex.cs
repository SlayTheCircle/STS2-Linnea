namespace LinneaMod.Content.Cards;

/// <summary>灵敏直觉：按原案与升级表实现。</summary>
[RegisterCard(typeof(LinneaCardPool))]
public sealed class KeenReflex : LinneaCardBase
{
    protected override IEnumerable<DynamicVar> CanonicalVars => new DynamicVar[] { new BlockVar(3m, ValueProp.Move), new DynamicVar("NextBlock", 9m) };
    public override bool GainsBlock => true;

    public KeenReflex() : base(1, CardType.Skill, CardRarity.Common, TargetType.Self) { }
    protected override async Task OnPlay(PlayerChoiceContext context, CardPlay play)
    {
        await CreatureCmd.GainBlock(Owner.Creature, DynamicVars.Block, play);
        await PowerCmd.Apply<ReflexNextTurnPower>(context, Owner.Creature, DynamicVars["NextBlock"].BaseValue, Owner.Creature, this);
    }
    protected override void OnUpgrade()
    {
        DynamicVars["Block"].UpgradeValueBy(2m);
        DynamicVars["NextBlock"].UpgradeValueBy(4m);
    }
}
