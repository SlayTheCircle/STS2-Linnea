namespace LinneaMod.Content.Powers;

[RegisterPower]
public sealed class AllInPower : LinneaPowerBase
{
    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Single;


    protected override decimal ModifyDamageMultiplicativeCore(Creature? target, decimal amount, ValueProp props, Creature? dealer, CardModel? card)
        => target == Owner || (dealer == Owner && card is Lumi) ? 2m : 1m;
    public override async Task AfterSideTurnEnd(PlayerChoiceContext context, CombatSide side, IEnumerable<Creature> participants)
    {
        if (participants.Contains(Owner)) await PowerCmd.Remove(this);
    }
}
