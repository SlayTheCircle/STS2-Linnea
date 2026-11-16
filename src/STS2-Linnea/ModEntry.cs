using STS2RitsuLib.Keywords;
using HarmonyLib;
using MegaCrit.Sts2.Core.Modding;
using STS2RitsuLib.Content;
using STS2RitsuLib.Interop;

namespace LinneaMod;

/// <summary>
/// Mod 入口。加载清单见仓库根 STS2-Linnea.json;本文件只做注册接线,
/// 游戏内容逻辑全部位于 Content/ 下的纯模型类中。
/// </summary>
[ModInitializer("Init")]
public static class ModEntry
{
    public const string ModId = "STS2-Linnea";

    public static void Init()
    {
        // 归属声明:让本程序集内的 RitsuLib 自动注册特性([RegisterCard]/[RegisterCharacter]/…)正确归属到本 mod。
        // 必须第一条;模型由特性注册；世界线在类型发现后接入复合ID，本入口只登记模块。
        ModTypeDiscoveryHub.RegisterModAssembly(ModId, typeof(ModEntry).Assembly);

        // 机制关键词注册(有机制名词时启用;文本在 localization/{lang}/card_keywords.json):
        //   ModKeywordRegistry.For(ModId).RegisterCardKeywordOwnedByLocNamespace("MYMECHANIC");
        // 常量与悬停助手见 Content/Keywords/LinneaKeywords.cs。

        ModKeywordRegistry.For(ModId).RegisterCardKeywordOwnedByLocNamespace("ENCOURAGE");
        ModKeywordRegistry.For(ModId).RegisterCardKeywordOwnedByLocNamespace("FOREKNOWLEDGE");
        ModKeywordRegistry.For(ModId).RegisterCardKeywordOwnedByLocNamespace("EXCAVATE");

        Content.Mechanics.LinneaRunRecord.Register();
        Content.Characters.LinneaCharacterAssets.Register();
        ModTypeDiscoveryHub.RegisterContributor(new Content.Timeline.LinneaTimeline());

        // 自带 Harmony 补丁:ModInitializer 通道与游戏自动 PatchAll 是官方二选一语义——
        // 本类有 [ModInitializer] 则游戏只调 Init() 不再自动 PatchAll;有补丁类时必须在此手动补。
        new Harmony(ModId + ".patches").PatchAll(typeof(ModEntry).Assembly);
    }
}
