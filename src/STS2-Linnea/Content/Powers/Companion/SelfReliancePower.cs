namespace LinneaMod.Content.Powers;

[RegisterPower]
public sealed class SelfReliancePower : LinneaPowerBase
{
    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Counter;

    public override async Task BeforeSideTurnEnd(PlayerChoiceContext context, CombatSide side, IEnumerable<Creature> participants)
    {
        if (participants.Contains(Owner) && !PileType.Hand.GetPile(Owner.Player!).Cards.OfType<Lumi>().Any())
            await CreatureCmd.GainBlock(Owner, Amount, ValueProp.Unpowered, null);
    }
}
