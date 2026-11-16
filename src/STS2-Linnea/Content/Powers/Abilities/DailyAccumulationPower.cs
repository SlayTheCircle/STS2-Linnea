namespace LinneaMod.Content.Powers;

[RegisterPower]
public sealed class DailyAccumulationPower : LinneaPowerBase, IForeknowledgeListener
{
    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Counter;


    public async Task AfterForeknowledgeGained(PlayerChoiceContext context, Player player, int amount)
    {
        if (player.Creature == Owner && Owner.CombatState?.CurrentSide == Owner.Side)
            await LumiCompanion.Encourage(context, player, amount * Amount, null);
    }
    public Task AfterForeknowledgeSpent(PlayerChoiceContext context, Player player, int amount) => Task.CompletedTask;
}
