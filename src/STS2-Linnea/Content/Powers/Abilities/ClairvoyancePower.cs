namespace LinneaMod.Content.Powers;

[RegisterPower]
public sealed class ClairvoyancePower : LinneaPowerBase
{
    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Counter;


    public override async Task BeforeSideTurnEnd(PlayerChoiceContext context, CombatSide side, IEnumerable<Creature> participants)
    {
        if (participants.Contains(Owner)) await CreatureCmd.GainBlock(Owner,
            Amount * Foreknowledge.Amount(Owner.Player!), ValueProp.Unpowered, null);
    }
}
