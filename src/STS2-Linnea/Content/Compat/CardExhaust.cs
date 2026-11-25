namespace LinneaMod.Content.Mechanics;

/// <summary>0.107.1 has no exhaust result; retain the native early-exit conditions.</summary>
internal static class LinneaCardExhaust
{
    public static async Task<bool> Try(PlayerChoiceContext context, CardModel card)
    {
        return await CardCmd.Exhaust(context, card) is not null;
    }
}
