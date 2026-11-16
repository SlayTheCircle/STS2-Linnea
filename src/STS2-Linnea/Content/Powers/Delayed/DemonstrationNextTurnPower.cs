namespace LinneaMod.Content.Powers;

[RegisterPower]
public sealed class DemonstrationNextTurnPower : NextTurnPowerBase
{

    public override async Task BeforeSideTurnEnd(PlayerChoiceContext context, CombatSide side, IEnumerable<Creature> participants)
    {
        if (!participants.Contains(Owner) || !IsDue) return;
        await PowerCmd.Remove(this);
        if (LumiCompanion.Find(Owner.Player!)?.Pile?.Type == PileType.Hand)
            await LumiCompanion.Encourage(context, Owner.Player!, Amount, null);
    }
}
