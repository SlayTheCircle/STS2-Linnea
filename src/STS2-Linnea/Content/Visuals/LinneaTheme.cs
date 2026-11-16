using Godot;

namespace LinneaMod.Content.Visuals;

internal static class LinneaTheme
{
    public const string EnergyColorName = "linnea";
    public const string BigEnergyIconPath = "res://STS2-Linnea/images/energy/linnea_energy_big.png";
    public const string TextEnergyIconPath = "res://STS2-Linnea/images/energy/linnea_energy_text.png";
    public static Color Primary => new("F4BACD");
    public static Color Outline => new("7D3D55");
    public static Color Ink => new("BB657D");
    public static Material FrameMaterial => _frameMaterial ??= ResourceLoader.Load<ShaderMaterial>("res://STS2-Linnea/materials/cards/linnea_frame.tres");
    private static Material? _frameMaterial;
}
