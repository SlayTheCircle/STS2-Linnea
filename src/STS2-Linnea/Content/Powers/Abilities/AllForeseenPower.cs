namespace LinneaMod.Content.Powers;

[RegisterPower]
public sealed class AllForeseenPower : LinneaPowerBase, IForeknowledgeListener
{
    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Single;


    private int _spent;
    public override PowerInstanceType InstanceType => PowerInstanceType.Instanced;
    public Task AfterForeknowledgeGained(PlayerChoiceContext context, Player player, int amount) => Task.CompletedTask;
    public async Task AfterForeknowledgeSpent(PlayerChoiceContext context, Player player, int amount)
    {
        if (player.Creature != Owner) return;
        _spent += amount;
        int ores = _spent / Amount;
        _spent %= Amount;
        await LinneaCards.Ore(player, PileType.Discard, ores);
    }
}
