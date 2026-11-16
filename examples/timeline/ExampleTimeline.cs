using MegaCrit.Sts2.Core.Models.Characters;
using MegaCrit.Sts2.Core.Timeline;
using STS2RitsuLib.Timeline;
using STS2RitsuLib.Unlocks;
using LinneaMod.Content.Characters;

namespace LinneaMod.Examples.Timeline;

public static class ExampleTimeline
{
    // 由 ExampleTimelineDiscovery 在内置特性注册完成后调用一次。
    public static void Register()
    {
        var timeline = ModTimelineRegistry.For(ModEntry.ModId);
        timeline.RegisterStory<ExampleStory>();
        timeline.RegisterStoryEpoch<ExampleStory, ExampleCharacterEpoch>();
        timeline.RegisterStoryEpoch<ExampleStory, ExampleCardsEpoch>();
        ModTimelineLayoutRegistry.RegisterAutoTimelineSlotBeforeEraColumn(
            typeof(ExampleCharacterEpoch), EpochEra.FarFuture0, ModEntry.ModId);
        ModTimelineLayoutRegistry.RegisterAutoTimelineSlotInEpochColumn(
            typeof(ExampleCardsEpoch), typeof(ExampleCharacterEpoch), ModEntry.ModId);

        var unlocks = ModUnlockRegistry.For(ModEntry.ModId);
        unlocks.RequireEpoch<Linnea, ExampleCharacterEpoch>();
        unlocks.UnlockCharacterAfterRunAs(typeof(Ironclad), typeof(ExampleCharacterEpoch));
        foreach (var cardType in new ExampleCardsEpoch().EnumerateUnlockCardTypes())
            unlocks.RequireEpoch(cardType, typeof(ExampleCardsEpoch));
        unlocks.UnlockEpochAfterWinAs<Linnea, ExampleCardsEpoch>();
    }
}
