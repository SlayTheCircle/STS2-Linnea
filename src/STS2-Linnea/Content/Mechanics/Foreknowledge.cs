namespace LinneaMod.Content.Mechanics;

public static class Foreknowledge
{
    public static int Amount(Player player) => player.Creature.GetPower<ForeknowledgePower>()?.Amount ?? 0;

    public static async Task Gain(PlayerChoiceContext context, Player player, decimal amount, CardModel? source)
    {
        if (amount <= 0) return;
        int before = player.Creature.GetPower<ForeknowledgeProgressPower>()?.Amount ?? 0;
        await PowerCmd.Apply<ForeknowledgePower>(context, player.Creature, amount, player.Creature, source);
        ForeknowledgeProgressPower? progress = await PowerCmd.Apply<ForeknowledgeProgressPower>(context, player.Creature, amount, player.Creature, source);
        int rewards = ((progress?.Amount ?? before) / 7) - before / 7;
        int gained = (progress?.Amount ?? before) - before;
        foreach (var listener in Listeners(player))
            await listener.AfterForeknowledgeGained(context, player, gained);
        for (int i = 0; i < rewards; i++) await ResolveReward(context, player);
    }

    public static async Task<int> Spend(PlayerChoiceContext context, Player player, int amount)
    {
        ForeknowledgePower? power = player.Creature.GetPower<ForeknowledgePower>();
        int spent = Math.Min(Math.Max(0, amount), power?.Amount ?? 0);
        if (power is not null && spent > 0)
        {
            await PowerCmd.ModifyAmount(context, power, -spent, player.Creature, null);
            foreach (var listener in Listeners(player))
                await listener.AfterForeknowledgeSpent(context, player, spent);
        }
        return spent;
    }

    private static IForeknowledgeListener[] Listeners(Player player)
        => player.Creature.Powers.Cast<AbstractModel>()
            .Concat(player.PlayerCombatState?.AllCards ?? Enumerable.Empty<CardModel>())
            .OfType<IForeknowledgeListener>().ToArray();

    private static async Task ResolveReward(PlayerChoiceContext context, Player player)
    {
        CardModel? top = PileType.Draw.GetPile(player).Cards.FirstOrDefault();
        if (top is null) return;
        var prefs = new CardSelectorPrefs(new LocString("card_selection", "STS2_LINNEA_FOREKNOWLEDGE_CHOICE"), 0, 1)
            { RequireManualConfirmation = true };
        // The actual top card is shown; select it to exhaust, confirm zero to draw.
        var selected = await CardSelectCmd.FromSimpleGrid(context, new[] { top }, player, prefs);
        if (selected.Contains(top) && top.Pile?.Type == PileType.Draw) await CardCmd.Exhaust(context, top);
        else await CardPileCmd.Draw(context, 1, player);
    }
}
