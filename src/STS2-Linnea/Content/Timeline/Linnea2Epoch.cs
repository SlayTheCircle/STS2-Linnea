using STS2RitsuLib.Scaffolding.Content;
using STS2RitsuLib.Timeline.Scaffolding;

namespace LinneaMod.Content.Timeline;

public sealed class Linnea2Epoch : CardUnlockEpochTemplate
{
    public override string Id => "STS2_LINNEA_EPOCH_2";
    public override string StoryId => "Linnea";
    public override EpochAssetProfile AssetProfile => new(
        PackedPortraitPath: "res://STS2-Linnea/images/timeline/sts2_linnea_epoch_2_thumb.png",
        BigPortraitPath: "res://images/timeline/epoch_portraits/sts2_linnea_epoch_2.png");
    protected override IEnumerable<Type> CardTypes => new[] { typeof(Cards.ForeknowledgeForm), typeof(Cards.HardStudy), typeof(Cards.DiscoverTheVein) };
}
