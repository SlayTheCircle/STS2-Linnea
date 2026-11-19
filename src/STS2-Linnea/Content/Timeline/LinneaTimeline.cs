using System.Reflection;
using HarmonyLib;
using STS2RitsuLib.Interop;
using MegaCrit.Sts2.Core.Models.Characters;
using MegaCrit.Sts2.Core.Timeline;
using STS2RitsuLib.Timeline;
using STS2RitsuLib.Unlocks;

namespace LinneaMod.Content.Timeline;

internal sealed class LinneaTimeline : IModTypeDiscoveryContributor
{
    public void Contribute(Harmony harmony, IReadOnlyDictionary<string, Assembly> assemblies, Type modType)
    {
        // Compound model IDs exist only after the built-in attribute registration pass.
        if (modType == typeof(LinneaTimeline)) Register();
    }

    private static void Register()
    {
        var timeline = ModTimelineRegistry.For(ModEntry.ModId);
        timeline.RegisterStory<LinneaStory>();
        timeline.RegisterStoryEpoch<LinneaStory, Linnea1Epoch>();
        timeline.RegisterStoryEpoch<LinneaStory, Linnea2Epoch>();
        timeline.RegisterStoryEpoch<LinneaStory, Linnea3Epoch>();
        timeline.RegisterStoryEpoch<LinneaStory, Linnea4Epoch>();
        ModTimelineLayoutRegistry.RegisterAutoTimelineSlotBeforeEraColumn(typeof(Linnea1Epoch), EpochEra.FarFuture0, ModEntry.ModId);
        ModTimelineLayoutRegistry.RegisterAutoTimelineSlotInEpochColumn(typeof(Linnea2Epoch), typeof(Linnea1Epoch), ModEntry.ModId);
        ModTimelineLayoutRegistry.RegisterAutoTimelineSlotInEpochColumn(typeof(Linnea3Epoch), typeof(Linnea1Epoch), ModEntry.ModId);
        ModTimelineLayoutRegistry.RegisterAutoTimelineSlotInEpochColumn(typeof(Linnea4Epoch), typeof(Linnea1Epoch), ModEntry.ModId);

        var unlocks = ModUnlockRegistry.For(ModEntry.ModId);
        unlocks.UnlockCharacterAfterRunAs(typeof(Ironclad), typeof(Linnea1Epoch));
        foreach (var type in new Linnea2Epoch().EnumerateUnlockCardTypes())
            unlocks.RequireEpoch(type, typeof(Linnea2Epoch));
        foreach (var type in new Linnea3Epoch().EnumerateUnlockRelicTypes())
            unlocks.RequireEpoch(type, typeof(Linnea3Epoch));
        foreach (var type in Linnea4Epoch.UnlockTypes)
            unlocks.RequireEpoch(type, typeof(Linnea4Epoch));
        unlocks.UnlockEpochAfterWinAs<Characters.Linnea, Linnea2Epoch>();
        unlocks.UnlockEpochAfterBossVictories<Characters.Linnea, Linnea3Epoch>(3);
        unlocks.UnlockEpochAfterAscensionOneWin<Characters.Linnea, Linnea4Epoch>();
    }
}
