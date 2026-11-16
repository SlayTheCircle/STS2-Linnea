using LinneaMod.Content.Visuals;
using Godot;
using STS2RitsuLib.Scaffolding.Content;

namespace LinneaMod.Content.CardPools;

/// <summary>
/// 卡池(TypeList 模式):池成员由各卡类上的 [RegisterCard(typeof(LinneaCardPool))] 特性自动聚合,
/// 本类只负责主题属性。增删卡 = 增删卡类文件,无需改动本文件。
/// 能量图标不覆写走原版默认;自备图标后覆写 Big/TextEnergyIconPath。
/// 卡框着色:vanilla 全部卡框共用一张底图,颜色即 HSV 着色参数(h, s, v)。
/// </summary>
public sealed class LinneaCardPool : TypeListCardPoolModel
{
    public override string Title => "linnea";

    public override string EnergyColorName => LinneaTheme.EnergyColorName;

    public override Material? PoolFrameMaterial => LinneaTheme.FrameMaterial;

    public override string? BigEnergyIconPath => LinneaTheme.BigEnergyIconPath;
    public override string? TextEnergyIconPath => LinneaTheme.TextEnergyIconPath;

    public override Color DeckEntryCardColor => LinneaTheme.Primary;

    public override Color EnergyOutlineColor => LinneaTheme.Outline;

    public override bool IsColorless => false;
}
