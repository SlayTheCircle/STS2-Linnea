namespace LinneaMod.Content.Powers;

[RegisterPower]
public sealed class FleetingNextTurnPower : NextTurnPowerBase
{
    protected override IEnumerable<DynamicVar> CanonicalVars => new[] { new DynamicVar("Spend", 7m) };
    public int ForeknowledgeCost => DynamicVars["Spend"].IntValue;
    public void SetForeknowledgeCost(int amount) { AssertMutable(); DynamicVars["Spend"].BaseValue = amount; }

    public override async Task AfterPlayerTurnStart(PlayerChoiceContext context, Player player)
    {
        if (player.Creature != Owner || !IsDue) return;
        await PowerCmd.Remove(this);
        foreach (var enemy in Owner.CombatState!.HittableEnemies.ToArray())
        {
            if (!LinneaCards.Attacking(enemy))
                await CreatureCmd.Damage(context, enemy, Amount, ValueProp.Unpowered, Owner);
            else if (Foreknowledge.Amount(player) >= ForeknowledgeCost)
            {
                await Foreknowledge.Spend(context, player, ForeknowledgeCost);
                if (enemy.IsHittable) await CreatureCmd.Stun(enemy);
            }
        }
    }
}
