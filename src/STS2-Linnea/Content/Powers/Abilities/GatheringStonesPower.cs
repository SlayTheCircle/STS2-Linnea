namespace LinneaMod.Content.Powers;

[RegisterPower]
public sealed class GatheringStonesPower : LinneaPowerBase
{
    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Counter;


    public override async Task BeforeSideTurnEnd(PlayerChoiceContext context, CombatSide side, IEnumerable<Creature> participants)
    {
        if (!participants.Contains(Owner)) return;
        int statuses = LinneaCards.Hand(Owner.Player!).Count(c => c.Type == CardType.Status);
        await CreatureCmd.GainBlock(Owner, Amount * statuses, ValueProp.Unpowered, null);
        if (statuses == 0)
            await CardPileCmd.AddGeneratedCardToCombat(Owner.CombatState!.CreateCard<Ore>(Owner.Player!), PileType.Discard, Owner.Player!);
    }
}
