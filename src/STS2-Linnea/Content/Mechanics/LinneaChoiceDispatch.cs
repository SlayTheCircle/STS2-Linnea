namespace LinneaMod.Content.Mechanics;

/// <summary>Bridge native hooks without a context to the synchronized choice action.</summary>
internal static class LinneaChoiceDispatch
{
    public static async Task Run(AbstractModel source, Player player, Func<PlayerChoiceContext, Task> effect)
    {
        if (player.Creature.CombatState is not { } combat || !CombatManager.Instance.IsInProgress) return;
        if (MegaCrit.Sts2.Core.Context.LocalContext.NetId is { } localId)
        {
            var context = new HookPlayerChoiceContext(source, localId, combat,
                MegaCrit.Sts2.Core.Entities.Multiplayer.GameActionType.Combat);
            Task task = effect(context);
            if (await context.AssignTaskAndWaitForPauseOrCompletion(task)) await task;
        }
        else await effect(new BlockingPlayerChoiceContext());
    }
}
