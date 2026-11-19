using MegaCrit.Sts2.Core.Timeline;
using STS2RitsuLib.Scaffolding.Content;
using STS2RitsuLib.Timeline.Scaffolding;

namespace LinneaMod.Content.Timeline;

public sealed class Linnea1Epoch : ModEpochTemplate
{
    public override string Id => "STS2_LINNEA_EPOCH_1";
    public override string StoryId => "Linnea";
    public override EpochAssetProfile AssetProfile => new(
        PackedPortraitPath: "res://STS2-Linnea/images/timeline/sts2_linnea_epoch_1_thumb.png",
        BigPortraitPath: "res://images/timeline/epoch_portraits/sts2_linnea_epoch_1.png");

    public override EpochModel[] GetTimelineExpansion() => new[] { typeof(Linnea2Epoch), typeof(Linnea3Epoch), typeof(Linnea4Epoch) }
        .Select(type => EpochModel.Get(EpochModel.GetId(type))).ToArray();

    public override void QueueUnlocks()
    {
        EpochModel[] expansion = GetTimelineExpansion();
        if (expansion.Length > 0) QueueTimelineExpansion(expansion);
    }
}
