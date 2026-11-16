using System;
using System.Collections.Generic;
using System.Reflection;
using HarmonyLib;
using STS2RitsuLib.Interop;

namespace LinneaMod.Examples.Timeline;

public sealed class ExampleTimelineDiscovery : IModTypeDiscoveryContributor
{
    public void Contribute(Harmony harmony, IReadOnlyDictionary<string, Assembly> assemblies, Type modType)
    {
        if (modType == typeof(ExampleTimelineDiscovery)) ExampleTimeline.Register();
    }
}
