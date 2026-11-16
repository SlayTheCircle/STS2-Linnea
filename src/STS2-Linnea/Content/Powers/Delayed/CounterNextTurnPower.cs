namespace LinneaMod.Content.Powers;

[RegisterPower]
public sealed class CounterNextTurnPower : NextTurnPowerBase
{

    public override async Task AfterPlayerTurnStart(PlayerChoiceContext context, Player player)
    {
        if (player.Creature != Owner || !IsDue) return;
        await PowerCmd.Remove(this);
        foreach (var enemy in Owner.CombatState!.HittableEnemies.Where(LinneaCards.Attacking).ToArray())
        {
            await PowerCmd.Apply<WeakPower>(context, enemy, Amount, Owner, null);
            await PowerCmd.Apply<VulnerablePower>(context, enemy, Amount, Owner, null);
        }
    }
}
