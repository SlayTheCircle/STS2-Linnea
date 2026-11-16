using HarmonyLib;
using MegaCrit.Sts2.Core.Hooks;

namespace LinneaMod.Content.Mechanics;

[HarmonyPatch(typeof(Hook), nameof(Hook.BeforeCombatStart))]
internal static class LinneaBattleLifecyclePatch
{
    private static void Postfix(IRunState runState, ICombatState? combatState, ref Task __result)
        => __result = Initialize(__result, runState, combatState);
    private static async Task Initialize(Task original, IRunState run, ICombatState? combat)
    {
        await original;
        if (combat is null) return;
        // Applies to every player so cards obtained from another pool retain correct HP history.
        foreach (Player player in run.Players)
            if (!player.Creature.HasPower<LinneaBattleRecordPower>())
                await PowerCmd.Apply<LinneaBattleRecordPower>(new ThrowingPlayerChoiceContext(), player.Creature, 1, player.Creature, null);
    }
}
