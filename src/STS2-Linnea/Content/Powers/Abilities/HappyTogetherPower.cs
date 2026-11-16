namespace LinneaMod.Content.Powers;

[RegisterPower]
public sealed class HappyTogetherPower : LinneaPowerBase
{
    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Counter;


    public override async Task AfterPlayerTurnStart(PlayerChoiceContext context, Player player)
    {
        if (player.Creature != Owner) return;
        await CreatureCmd.GainBlock(Owner, Amount, ValueProp.Unpowered, null);
        await CreatureCmd.Heal(Owner, Amount);
    }
}
