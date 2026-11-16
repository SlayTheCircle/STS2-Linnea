using HarmonyLib;
using MegaCrit.Sts2.Core.Events;

namespace LinneaMod.Content.Events;

[HarmonyPatch(typeof(EventModel), "GenerateInitialOptionsWrapper")]
internal static class BrainLeechKnowledgeOptionPatch
{
    private static void Postfix(EventModel __instance, ref IReadOnlyList<EventOption> __result)
    {
        if (!VanillaEventCompletion.IsAvailable || __instance is not MegaCrit.Sts2.Core.Models.Events.BrainLeech
            || __instance.Owner?.Character is not Characters.Linnea) return;
        bool downgrade = __instance.Owner.Deck.Cards.Any(card => card.IsUpgraded);
        var extra = new EventOption(__instance, downgrade ? () => Exchange(__instance) : null,
            "BRAIN_LEECH.pages.INITIAL.options." + (downgrade ? "LINNEA_EXCHANGE" : "LINNEA_EXCHANGE_LOCKED"));
        __result = __result.Append(extra).ToArray();
    }
    private static Task Exchange(EventModel evt)
    {
        if (!EventDeckChanges.DowngradeRandom(evt.Owner!, evt.Rng)) return Task.CompletedTask;
        EventDeckChanges.UpgradeRandom(evt.Owner!, evt.Rng, 2);
        VanillaEventCompletion.Finish(evt, "BRAIN_LEECH.pages.LINNEA_EXCHANGE.description");
        return Task.CompletedTask;
    }
}
