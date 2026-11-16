using System;
using System.Collections.Generic;
using STS2RitsuLib.Scaffolding.Content;
using STS2RitsuLib.Timeline.Scaffolding;
using LinneaMod.Content.Characters;

namespace LinneaMod.Examples.Timeline;

public sealed class ExampleCharacterEpoch : CharacterUnlockEpochTemplate<Linnea>
{
    public override string Id => "STS2_LINNEA_EXAMPLE_CHARACTER";
    public override string StoryId => ExampleStory.Key;
    protected override IEnumerable<Type> ExpansionEpochTypes => new[] { typeof(ExampleCardsEpoch) };
    public override EpochAssetProfile AssetProfile => new(
        PackedPortraitPath: "res://STS2-Linnea/images/timeline/example_character.png",
        BigPortraitPath: "res://images/timeline/epoch_portraits/STS2_LINNEA_EXAMPLE_CHARACTER.png");
}
