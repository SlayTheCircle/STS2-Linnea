using STS2RitsuLib.Scaffolding.Content;
using STS2RitsuLib.Timeline.Scaffolding;

namespace LinneaMod.Content.Timeline;

public sealed class Linnea1Epoch : CharacterUnlockEpochTemplate<Characters.Linnea>
{
    public override string Id => "STS2_LINNEA_EPOCH_1";
    public override string StoryId => "Linnea";
    public override EpochAssetProfile AssetProfile => new(
        PackedPortraitPath: "res://STS2-Linnea/images/timeline/sts2_linnea_epoch_1_thumb.png",
        BigPortraitPath: "res://images/timeline/epoch_portraits/sts2_linnea_epoch_1.png");
    protected override IEnumerable<Type> ExpansionEpochTypes => new[] { typeof(Linnea2Epoch), typeof(Linnea3Epoch), typeof(Linnea4Epoch) };
}
