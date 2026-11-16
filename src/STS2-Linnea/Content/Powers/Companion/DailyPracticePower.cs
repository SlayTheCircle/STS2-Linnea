namespace LinneaMod.Content.Powers;

[RegisterPower]
public sealed class DailyPracticePower : LinneaPowerBase
{
    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Counter;

    public override async Task AfterPlayerTurnStart(PlayerChoiceContext context, Player player)
    {
        if (player == Owner.Player) await LumiCompanion.Encourage(context, player, Amount, null);
    }
}
