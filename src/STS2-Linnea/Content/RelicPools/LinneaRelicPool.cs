using LinneaMod.Content.Visuals;
using Godot;
using STS2RitsuLib.Scaffolding.Content;

namespace LinneaMod.Content.RelicPools;

/// <summary>遗物池(TypeList 模式):成员由 [RegisterRelic(typeof(LinneaRelicPool))] 特性聚合。</summary>
public sealed class LinneaRelicPool : TypeListRelicPoolModel
{
    public override string EnergyColorName => LinneaTheme.EnergyColorName;

    public override Color LabOutlineColor => LinneaTheme.Primary;
}
