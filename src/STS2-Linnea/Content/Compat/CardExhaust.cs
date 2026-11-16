namespace LinneaMod.Content.Mechanics;

/// <summary>0.107.1 has no exhaust result; retain the native early-exit conditions.</summary>
internal static class LinneaCardExhaust
{
    public static async Task<bool> Try(PlayerChoiceContext context, CardModel card)
    {
#if MOD_GAME_0107_1
        if (CombatManager.Instance.IsOverOrEnding || card.Owner.Creature.IsDead) return false;
        await CardCmd.Exhaust(context, card);
        return true;
#else
        return await CardCmd.Exhaust(context, card) is not null;
#endif
    }
}
