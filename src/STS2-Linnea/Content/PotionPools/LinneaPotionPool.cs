using LinneaMod.Content.Visuals;
using Godot;
using STS2RitsuLib.Scaffolding.Content;

namespace LinneaMod.Content.PotionPools;

/// <summary>药水池(TypeList 模式):成员由 [RegisterPotion(typeof(LinneaPotionPool))] 特性聚合。</summary>
public sealed class LinneaPotionPool : TypeListPotionPoolModel
{
    public override string EnergyColorName => LinneaTheme.EnergyColorName;

    public override Color LabOutlineColor => LinneaTheme.Primary;
}
