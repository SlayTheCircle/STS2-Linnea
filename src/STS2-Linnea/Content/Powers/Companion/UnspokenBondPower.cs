namespace LinneaMod.Content.Powers;

[RegisterPower]
public sealed class UnspokenBondPower : LinneaPowerBase
{
    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Counter;

    public override async Task BeforeSideTurnEnd(PlayerChoiceContext context, CombatSide side, IEnumerable<Creature> participants)
    {
        if (!participants.Contains(Owner)) return;
        for (int i = 0; i < Amount; i++)
        {
            Player player = Owner.Player!;
            if (LumiCompanion.Find(player)?.Pile?.Type == PileType.Hand)
                await LumiCompanion.Play(context, player, null);
            else
                await LumiCompanion.Recall(context, player);
        }
    }
}
