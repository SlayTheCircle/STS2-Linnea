namespace LinneaMod.Content.Mechanics;

public static class LumiCompanion
{
    public static Lumi? Find(Player player)
        => player.PlayerCombatState?.AllCards.OfType<Lumi>().FirstOrDefault();

    public static async Task<Lumi?> Generate(PlayerChoiceContext context, Player player)
    {
        if (Find(player) is not null || player.Creature.CombatState is not { } combat) return null;
        Lumi lumi = combat.CreateCard<Lumi>(player);
        await CardPileCmd.AddGeneratedCardToCombat(lumi, PileType.Hand, player);
        return lumi;
    }

    public static async Task<Lumi?> Recall(PlayerChoiceContext context, Player player)
    {
        Lumi? lumi = Find(player);
        if (lumi is null) return await Generate(context, player);
        if (lumi.Pile?.Type != PileType.Hand) await CardPileCmd.Add(lumi, PileType.Hand);
        return lumi;
    }

    public static async Task Encourage(PlayerChoiceContext context, Player player, decimal amount, CardModel? source)
        => await PowerCmd.Apply<EncouragementPower>(context, player.Creature, amount, player.Creature, source);

    public static async Task<decimal> Play(PlayerChoiceContext context, Player player, Creature? target)
    {
        if (Find(player) is not { Pile.Type: PileType.Hand } lumi) return 0;
        decimal before = lumi.CumulativeDamage;
        await CardCmd.AutoPlay(context, lumi, target);
        return lumi.CumulativeDamage - before;
    }
}
