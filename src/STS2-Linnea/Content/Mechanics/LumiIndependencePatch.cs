using HarmonyLib;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Models.Powers;
using LinneaMod.Content.Cards;

namespace LinneaMod.Content.Mechanics;

/// <summary>露米独立结算：莉奈娅的虚弱不降低露米伤害，其他攻击修正保持原管线。</summary>
[HarmonyPatch(typeof(WeakPower), nameof(WeakPower.ModifyDamageMultiplicative))]
internal static class LumiIndependencePatch
{
    private static bool Prefix(CardModel? cardSource, ref decimal __result)
    {
        if (cardSource is not Lumi) return true;
        __result = 1m;
        return false;
    }
}
