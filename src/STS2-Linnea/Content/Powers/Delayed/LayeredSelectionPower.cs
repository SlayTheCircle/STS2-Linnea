namespace LinneaMod.Content.Powers;

[RegisterPower]
public sealed class LayeredSelectionPower : NextTurnPowerBase
{

    public override async Task AfterPlayerTurnStart(PlayerChoiceContext context, Player player)
    {
        if (player.Creature != Owner || !IsDue) return;
        await PowerCmd.Remove(this);
        var hand = LinneaCards.Hand(player);
        int cards = hand.Count(c => c.Type == CardType.Status);
        if (hand.Length + cards >= CardPile.MaxCardsInHand) await CardCmd.Discard(context, hand.OfType<Ore>());
        await CardPileCmd.Draw(context, cards, player);
    }
}
