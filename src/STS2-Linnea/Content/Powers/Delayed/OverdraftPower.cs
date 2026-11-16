namespace LinneaMod.Content.Powers;

[RegisterPower]
public sealed class OverdraftPower : NextTurnPowerBase
{

    public override async Task AfterPlayerTurnStart(PlayerChoiceContext context, Player player)
    {
        if (player.Creature != Owner || !IsDue) return;
        await PowerCmd.Remove(this);
        int count = Math.Min(Amount, LinneaCards.Hand(player).Length);
        if (count > 0) await CardCmd.Discard(context, await CardSelectCmd.FromHandForDiscard(context, player, new CardSelectorPrefs(
            new LocString("card_selection", "STS2_LINNEA_OVERDRAFT"), count), null, this));
    }
}
