using HarmonyLib;
using MegaCrit.Sts2.Core.Hooks;
using STS2RitsuLib.RunData;

namespace LinneaMod.Content.Mechanics;

/// <summary>Run-scoped, serialized defeated model IDs, including kills before obtaining Bestiary.</summary>
public static class LinneaRunRecord
{
    private static RunSavedData<BestiaryData> _bestiary = null!;
    public static void Register() => _bestiary = RunSavedDataStore.For(ModEntry.ModId)
        .Register("bestiary", () => new BestiaryData());
    public static int Count(Player player) => player.RunState is RunState run ? _bestiary.Get(run).Defeated.Count : 0;
    internal static void Record(IRunState runState, Creature creature)
    {
        if (runState is not RunState run || creature.Side != CombatSide.Enemy || creature.Monster is null || creature.CurrentHp > 0) return;
        string id = creature.Monster.Id.ToString();
        _bestiary.Modify(run, data => { if (!data.Defeated.Contains(id)) data.Defeated.Add(id); });
    }
}

public sealed class BestiaryData
{
    public List<string> Defeated { get; set; } = new();
}

[HarmonyPatch(typeof(Hook), nameof(Hook.AfterDeath))]
internal static class BestiaryDeathPatch
{
    private static void Prefix(IRunState runState, Creature creature, bool wasRemovalPrevented)
    {
        if (!wasRemovalPrevented) LinneaRunRecord.Record(runState, creature);
    }
}
