namespace LinneaMod.Content.Cards;

/// <summary>须臾幻像：按原案与升级表实现。</summary>
[RegisterCard(typeof(LinneaCardPool))]
public sealed class FleetingMirage : LinneaCardBase
{
    public override IEnumerable<CardKeyword> CanonicalKeywords
    {
        get
        {
            HashSet<CardKeyword> result = new() {  };
            LinneaKeywords.AddTo(result, LinneaKeywords.Foreknowledge);
            return result;
        }
    }

    public override bool GainsBlock => true;
    protected override IEnumerable<DynamicVar> CanonicalVars => new DynamicVar[] {
        new CalculationBaseVar(0), new CalculationExtraVar(1),
        new CalculatedBlockVar(ValueProp.Move).WithMultiplier((card, _) => Foreknowledge.Amount(card.Owner)) };

    public FleetingMirage() : base(1, CardType.Skill, CardRarity.Rare, TargetType.Self) { }
    protected override async Task OnPlay(PlayerChoiceContext context, CardPlay play)
    {

        await CreatureCmd.GainBlock(Owner.Creature, DynamicVars.CalculatedBlock.Calculate(null), ValueProp.Move, play);
        await PowerCmd.Apply<MirageForeknowledgeGuardPower>(context, Owner.Creature, 1, Owner.Creature, this);
        await PowerCmd.Apply<BlurPower>(context, Owner.Creature, 1, Owner.Creature, this);
        var effect = await PowerCmd.Apply<MirageReturnPower>(context, Owner.Creature, 1, Owner.Creature, this);
        if (effect is not null) effect.ReturningCard = this;
    }
    protected override void OnUpgrade()
    {
        AddKeyword(CardKeyword.Retain);
    }
}
