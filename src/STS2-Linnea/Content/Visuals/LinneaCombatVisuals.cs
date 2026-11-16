using System.Runtime.CompilerServices;
using Godot;
using MegaCrit.Sts2.Core.Nodes.Combat;
using STS2RitsuLib.Scaffolding.Visuals.StateMachine;
using STS2RitsuLib.Scaffolding.Visuals.StateMachine.Backends;

namespace LinneaMod.Content.Visuals;

internal static class LinneaCombatVisuals
{
    private static readonly ConditionalWeakTable<Node, LinneaAnimationGraph> Graphs = new();
    public static ModAnimStateMachine Create(Node visualsRoot)
    {
        if (Graphs.TryGetValue(visualsRoot, out var existing)) return existing.Machine;
        var creature = (visualsRoot.GetParent() as NCreature)?.Entity
            ?? throw new InvalidOperationException("Combat visuals require an NCreature owner.");
        var player = visualsRoot.GetNode<AnimationPlayer>("Visuals/Rig/AnimationPlayer");
        var backend = new GodotAnimationPlayerBackend(player);
        var graph = new LinneaAnimationGraph(backend, creature);
        Graphs.Add(visualsRoot, graph);
        visualsRoot.TreeExiting += () =>
        {
            graph.Machine.Dispose();
            backend.Dispose();
            Graphs.Remove(visualsRoot);
        };
        return graph.Machine;
    }
}
