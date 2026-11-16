using STS2RitsuLib.Scaffolding.Content;
using STS2RitsuLib.Timeline.Scaffolding;

namespace LinneaMod.Content.Timeline;

public sealed class Linnea3Epoch : RelicUnlockEpochTemplate
{
    public override string Id => "STS2_LINNEA_EPOCH_3";
    public override string StoryId => "Linnea";
    public override EpochAssetProfile AssetProfile => new(
        PackedPortraitPath: "res://STS2-Linnea/images/timeline/sts2_linnea_epoch_3_thumb.png",
        BigPortraitPath: "res://images/timeline/epoch_portraits/sts2_linnea_epoch_3.png");
    protected override IEnumerable<Type> RelicTypes => new[] { typeof(Relics.GoldenHoundModel), typeof(Relics.PetBackpack), typeof(Relics.FestivalCertificate) };
}
