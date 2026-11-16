namespace LinneaMod.Content.Powers;

[RegisterPower]
public sealed class BlockNextTurnPower : NextTurnPowerBase
{

    public override async Task AfterPlayerTurnStart(PlayerChoiceContext context, Player player)
    {
        if (player.Creature != Owner || !IsDue) return;
        await PowerCmd.Remove(this);
        await CreatureCmd.GainBlock(Owner, Amount, ValueProp.Unpowered, null);
    }
}
