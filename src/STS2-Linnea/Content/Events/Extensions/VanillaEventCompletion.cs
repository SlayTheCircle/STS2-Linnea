using HarmonyLib;
using MegaCrit.Sts2.Core.Logging;

namespace LinneaMod.Content.Events;

/// <summary>The only protected native entry point needed by vanilla option extensions.</summary>
internal static class VanillaEventCompletion
{
    private static readonly Action<EventModel, LocString>? FinishDelegate = Bind();
    public static bool IsAvailable => FinishDelegate is not null;
    private static Action<EventModel, LocString>? Bind()
    {
        var method = AccessTools.Method(typeof(EventModel), "SetEventFinished", new[] { typeof(LocString) });
        if (method is not null) return method.CreateDelegate<Action<EventModel, LocString>>();
        Log.Error("[STS2-Linnea] Native event completion is unavailable; preserving vanilla options.");
        return null;
    }
    public static void Finish(EventModel evt, string key)
        => (FinishDelegate ?? throw new InvalidOperationException("Native event completion unavailable"))(evt, new LocString("events", key));
}
