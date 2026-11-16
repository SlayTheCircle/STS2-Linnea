using HarmonyLib;
using MegaCrit.Sts2.Core.Nodes.Cards;
using MegaCrit.Sts2.Core.Nodes.Screens;
using MegaCrit.Sts2.Core.Nodes.Screens.CardLibrary;

namespace LinneaMod.Content.Mechanics;

[HarmonyPatch(typeof(NCardPileScreen), "OnPileContentsChanged")]
internal static class ForeknowledgeViewPatch
{
    private static bool Prefix(NCardPileScreen __instance, NCardGrid ____grid)
    {
        CardPile pile = __instance.Pile;
        if (pile.Type != PileType.Draw || pile.Cards.Count == 0) return true;
        Player owner = pile.Cards[0].Owner;
        int known = Foreknowledge.Amount(owner);
        if (known <= 0) return true;
        // Unknown cards keep the same identity sort as vanilla; their real order is never leaked.
        List<CardModel> ordered = pile.Cards.Take(known).Concat(pile.Cards.Skip(known)
            .OrderBy(c => c.Rarity).ThenBy(c => c.Id.Entry, StringComparer.Ordinal)).ToList();
        ____grid.SetCards(ordered, pile.Type, new List<SortingOrders> { SortingOrders.Ascending });
        return false;
    }
}
