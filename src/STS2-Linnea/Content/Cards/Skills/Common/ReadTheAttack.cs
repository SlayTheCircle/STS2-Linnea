namespace LinneaMod.Content.Cards;

/// <summary>看破攻击：按原案与升级表实现。</summary>
[RegisterCard(typeof(LinneaCardPool))]
public sealed class ReadTheAttack : LinneaCardBase
{
    protected override IEnumerable<DynamicVar> CanonicalVars => new DynamicVar[] { new BlockVar(6m, ValueProp.Move), new DynamicVar("ExtraBlock", 6m) };
    public override bool GainsBlock => true;

    public ReadTheAttack() : base(1, CardType.Skill, CardRarity.Common, TargetType.Self) { }
    protected override async Task OnPlay(PlayerChoiceContext context, CardPlay play)
    {
        await CreatureCmd.GainBlock(Owner.Creature, DynamicVars.Block, play);
        if (LinneaCards.AnyAttacking(CombatState!)) await CreatureCmd.GainBlock(Owner.Creature, DynamicVars["ExtraBlock"].BaseValue, ValueProp.Move, play);
    }
    protected override void OnUpgrade()
    {
        DynamicVars["Block"].UpgradeValueBy(1m);
        DynamicVars["ExtraBlock"].UpgradeValueBy(3m);
    }
}
