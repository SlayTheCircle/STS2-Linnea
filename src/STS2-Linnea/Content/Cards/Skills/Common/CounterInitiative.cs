namespace LinneaMod.Content.Cards;

/// <summary>后发制人：按原案与升级表实现。</summary>
[RegisterCard(typeof(LinneaCardPool))]
public sealed class CounterInitiative : LinneaCardBase
{
    protected override IEnumerable<IHoverTip> AdditionalHoverTips => new IHoverTip[] { HoverTipFactory.FromPower<WeakPower>(), HoverTipFactory.FromPower<VulnerablePower>() };
    protected override IEnumerable<DynamicVar> CanonicalVars => new DynamicVar[] { new BlockVar(5m, ValueProp.Move), new DynamicVar("Debuff", 1m) };
    public override bool GainsBlock => true;

    public CounterInitiative() : base(1, CardType.Skill, CardRarity.Common, TargetType.Self) { }
    protected override async Task OnPlay(PlayerChoiceContext context, CardPlay play)
    {
        await CreatureCmd.GainBlock(Owner.Creature, DynamicVars.Block, play);
        await PowerCmd.Apply<CounterNextTurnPower>(context, Owner.Creature, DynamicVars["Debuff"].BaseValue, Owner.Creature, this);
    }
    protected override void OnUpgrade()
    {
        DynamicVars["Debuff"].UpgradeValueBy(1m);
    }
}
