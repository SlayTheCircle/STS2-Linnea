using STS2RitsuLib.Content;
using STS2RitsuLib.Scaffolding.Characters;

namespace LinneaMod.Content.Characters;

internal static class LinneaCharacterAssets
{
    internal const string SelectionSfx = "event:/sfx/ui/clicks/ui_click";
    public static void Register()
    {
        CharacterAssetProfile fallback = CharacterAssetProfiles.Ironclad();
        CharacterAssetProfile profile = new CharacterAssetProfile(
            new CharacterSceneAssetSet(
                "res://STS2-Linnea/scenes/characters/linnea_character.tscn",
                "res://STS2-Linnea/scenes/combat/linnea_energy_counter.tscn",
                "res://STS2-Linnea/scenes/characters/linnea_merchant.tscn",
                "res://STS2-Linnea/scenes/characters/linnea_rest_site.tscn"),
            new CharacterUiAssetSet(
                "res://STS2-Linnea/images/characters/linnea_icon.png",
                "res://STS2-Linnea/images/characters/linnea_icon_outline.png",
                "res://STS2-Linnea/scenes/characters/linnea_icon.tscn",
                "res://STS2-Linnea/scenes/characters/linnea_char_select_bg.tscn",
                "res://STS2-Linnea/images/characters/linnea_char_select.png",
                "res://STS2-Linnea/images/characters/linnea_char_select_locked.png",
                // 开局转场材质:留空会按条目名推导 mod 下不存在的路径直接炸开局(模板实测教训),指通用淡入淡出。
                "res://materials/transitions/fade_transition_mat.tres", "res://STS2-Linnea/images/characters/linnea_map_marker.png"),
            new CharacterVfxAssetSet("res://STS2-Linnea/scenes/vfx/linnea_card_trail.tscn"),
            null,
            new CharacterAudioAssetSet(CharacterSelectSfx: SelectionSfx),
            new CharacterMultiplayerAssetSet(
                "res://STS2-Linnea/images/hands/linnea_point.png",
                "res://STS2-Linnea/images/hands/linnea_rock.png",
                "res://STS2-Linnea/images/hands/linnea_paper.png",
                "res://STS2-Linnea/images/hands/linnea_scissors.png"));
        profile = CharacterAssetProfiles.FillMissingFrom(profile, fallback) with { Spine = null };
        string entry = ModContentRegistry.GetCompoundId(ModEntry.ModId, "character", nameof(Linnea)).ToLowerInvariant();
        ModContentRegistry.For(ModEntry.ModId).RegisterCharacterAssetReplacement(entry, profile);
    }
}
