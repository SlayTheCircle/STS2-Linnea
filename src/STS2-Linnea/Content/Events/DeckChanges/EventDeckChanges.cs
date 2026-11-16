using MegaCrit.Sts2.Core.Random;

namespace LinneaMod.Content.Events;

/// <summary>Event-local deterministic sampling without replacement from eligible deck cards.</summary>
internal static class EventDeckChanges
{
    public static bool DowngradeRandom(Player player, Rng rng)
    {
        var cards = player.Deck.Cards.Where(card => card.IsUpgraded).ToArray();
        if (cards.Length == 0) return false;
        CardCmd.Downgrade(cards[rng.NextInt(cards.Length)]);
        return true;
    }
    public static int UpgradeRandom(Player player, Rng rng, int count)
    {
        var cards = player.Deck.Cards.Where(card => card.IsUpgradable).ToList();
        int upgraded = 0;
        while (cards.Count > 0 && upgraded < count)
        {
            int index = rng.NextInt(cards.Count);
            var card = cards[index];
            cards.RemoveAt(index);
            CardCmd.Upgrade(card);
            upgraded++;
        }
        return upgraded;
    }
}
