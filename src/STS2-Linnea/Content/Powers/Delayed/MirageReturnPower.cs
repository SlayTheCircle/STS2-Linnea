namespace LinneaMod.Content.Powers;

[RegisterPower]
public sealed class MirageReturnPower : NextTurnPowerBase, IForeknowledgeListener
{
    public CardModel? ReturningCard { get; set; }
    public async Task AfterForeknowledgeGained(PlayerChoiceContext context, Player player, int amount)
    {
        if (player.Creature != Owner || !IsDue || amount <= 0) return;
        await PowerCmd.Remove(this);
        if (ReturningCard is { } card && card.Pile?.IsCombatPile == true && card.Pile.Type != PileType.Hand)
            await CardPileCmd.Add(card, PileType.Hand);
    }
    public Task AfterForeknowledgeSpent(PlayerChoiceContext context, Player player, int amount) => Task.CompletedTask;
}
