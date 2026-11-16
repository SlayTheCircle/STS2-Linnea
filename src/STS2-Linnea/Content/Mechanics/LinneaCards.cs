namespace LinneaMod.Content.Mechanics;

internal static class LinneaCards
{
    public static CardModel[] Hand(Player player) => PileType.Hand.GetPile(player).Cards.ToArray();
    public static bool IsColorless(CardModel card)
        => card.VisualCardPool is MegaCrit.Sts2.Core.Models.CardPools.ColorlessCardPool;
    public static bool Attacking(Creature enemy) => enemy.Monster?.IntendsToAttack == true;
    public static bool AnyAttacking(ICombatState combat) => combat.HittableEnemies.Any(Attacking);
    public static async Task DamageAll(PlayerChoiceContext context, Creature owner, decimal amount)
        => await CreatureCmd.Damage(context, owner.CombatState!.HittableEnemies, amount, ValueProp.Unpowered, owner);
    public static async Task DamageRandom(PlayerChoiceContext context, Creature owner, decimal amount)
    {
        var enemies = owner.CombatState!.HittableEnemies;
        if (enemies.Count > 0) await CreatureCmd.Damage(context,
            enemies[owner.Player!.RunState.Rng.CombatTargets.NextInt(enemies.Count)], amount, ValueProp.Unpowered, owner);
    }
    public static async Task Ore(Player player, PileType pile, int count)
    {
        if (player.Creature.CombatState is not { } combat) return;
        for (int i = 0; i < count; i++)
            await CardPileCmd.AddGeneratedCardToCombat(combat.CreateCard<Cards.Ore>(player), pile, player);
    }
}
