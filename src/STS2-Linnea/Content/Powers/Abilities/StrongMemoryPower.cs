namespace LinneaMod.Content.Powers;

[RegisterPower]
public sealed class StrongMemoryPower : LinneaPowerBase
{
    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Single;


    public override PowerInstanceType InstanceType => PowerInstanceType.Instanced;
    public override async Task AfterPlayerTurnStart(PlayerChoiceContext context, Player player)
    {
        if (player.Creature != Owner || Foreknowledge.Amount(player) < 5) return;
        await PlayerCmd.GainEnergy(1, player);
    }
}
