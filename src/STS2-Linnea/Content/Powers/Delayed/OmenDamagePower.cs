namespace LinneaMod.Content.Powers;

[RegisterPower]
public sealed class OmenDamagePower : NextTurnPowerBase
{

    public override async Task AfterPlayerTurnStart(PlayerChoiceContext context, Player player)
    {
        if (player.Creature != Owner || !IsDue) return;
        await PowerCmd.Remove(this);
        await LinneaCards.DamageAll(context, Owner, Amount);
    }
}
