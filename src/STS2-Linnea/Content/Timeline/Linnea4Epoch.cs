using STS2RitsuLib.Scaffolding.Content;
using STS2RitsuLib.Timeline.Scaffolding;

namespace LinneaMod.Content.Timeline;

public sealed class Linnea4Epoch : PotionUnlockEpochTemplate
{
    public override string Id => "STS2_LINNEA_EPOCH_4";
    public override string StoryId => "Linnea";
    public override EpochAssetProfile AssetProfile => new(
        PackedPortraitPath: "res://STS2-Linnea/images/timeline/sts2_linnea_epoch_4_thumb.png",
        BigPortraitPath: "res://images/timeline/epoch_portraits/sts2_linnea_epoch_4.png");
    internal static readonly IReadOnlyList<Type> UnlockTypes = Array.AsReadOnly(new[] { typeof(Potions.SummerOakJuice), typeof(Potions.BorderSoda), typeof(Potions.GemBerryJam) });
    protected override IEnumerable<Type> PotionTypes => UnlockTypes;
}
