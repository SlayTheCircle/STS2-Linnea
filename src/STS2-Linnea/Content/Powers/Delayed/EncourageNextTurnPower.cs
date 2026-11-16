namespace LinneaMod.Content.Powers;

[RegisterPower]
public sealed class EncourageNextTurnPower : NextTurnPowerBase
{

    public override async Task AfterPlayerTurnStart(PlayerChoiceContext context, Player player)
    {
        if (player.Creature != Owner || !IsDue) return;
        await PowerCmd.Remove(this);
        await LumiCompanion.Encourage(context, player, Amount, null);
    }
}
