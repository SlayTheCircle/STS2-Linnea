namespace LinneaMod.Content.Powers;

/// <summary>Each cast keeps its own payload and expires after its owner's next turn.</summary>
public abstract class NextTurnPowerBase : LinneaPowerBase
{
    private int _dueTurn;
    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Single;
    public override PowerInstanceType InstanceType => PowerInstanceType.Instanced;
    protected bool IsDue => Owner.Player?.PlayerCombatState?.TurnNumber == _dueTurn;
    public override Task AfterApplied(Creature? applier, CardModel? cardSource)
    {
        _dueTurn = (Owner.Player?.PlayerCombatState?.TurnNumber ?? 0) + 1;
        return Task.CompletedTask;
    }
    public override async Task AfterSideTurnEnd(PlayerChoiceContext context, CombatSide side, IEnumerable<Creature> participants)
    {
        if (participants.Contains(Owner) && IsDue) await PowerCmd.Remove(this);
    }
}
