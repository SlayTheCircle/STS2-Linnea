using HarmonyLib;
using MegaCrit.Sts2.Core.Events;

namespace LinneaMod.Content.Events;

/// <summary>Append only for the event's Linnea owner; preserve the two native choices.</summary>
[HarmonyPatch(typeof(EventModel), "GenerateInitialOptionsWrapper")]
internal static class SapphireCarryOptionPatch
{
    private static void Postfix(EventModel __instance, ref IReadOnlyList<EventOption> __result)
    {
        if (!VanillaEventCompletion.IsAvailable || __instance is not MegaCrit.Sts2.Core.Models.Events.SapphireSeed || __instance.Owner?.Character is not Characters.Linnea) return;
        var extra = new EventOption(__instance, () => Carry(__instance),
            "SAPPHIRE_SEED.pages.INITIAL.options.LINNEA_CARRY", HoverTipFactory.FromCard<SapphireSeedQuest>());
        __result = __result.Append(extra).ToArray();
    }
    private static async Task Carry(EventModel evt)
    {
        Player player = evt.Owner!;
        if (player.Deck.Cards.Any(c => c.IsUpgradable))
            foreach (var card in await CardSelectCmd.FromDeckForUpgrade(player,
                new CardSelectorPrefs(CardSelectorPrefs.UpgradeSelectionPrompt, 1))) CardCmd.Upgrade(card);
        await CardPileCmd.Add(player.RunState.CreateCard<SapphireSeedQuest>(player), player.Deck);
        VanillaEventCompletion.Finish(evt, "SAPPHIRE_SEED.pages.LINNEA_CARRY.description");
    }
}
