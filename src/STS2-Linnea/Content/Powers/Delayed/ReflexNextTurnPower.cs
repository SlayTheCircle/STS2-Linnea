namespace LinneaMod.Content.Powers;

[RegisterPower]
public sealed class ReflexNextTurnPower : NextTurnPowerBase
{

    public override async Task AfterPlayerTurnStart(PlayerChoiceContext context, Player player)
    {
        if (player.Creature != Owner || !IsDue) return;
        await PowerCmd.Remove(this);
        if (LinneaCards.AnyAttacking(Owner.CombatState!)) await CreatureCmd.GainBlock(Owner, Amount, ValueProp.Unpowered, null);
    }
}
