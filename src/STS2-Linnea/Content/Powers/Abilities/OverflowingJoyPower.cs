namespace LinneaMod.Content.Powers;

[RegisterPower]
public sealed class OverflowingJoyPower : LinneaPowerBase
{
    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Counter;


    public override async Task AfterCardChangedPiles(CardModel card, PileType oldPileType, AbstractModel? clonedBy)
    {
        if (card.Owner.Creature != Owner || card.Pile?.Type != PileType.Discard || oldPileType == PileType.Discard || Owner.CombatState?.CurrentSide != Owner.Side) return;
        await LinneaChoiceDispatch.Run(this, card.Owner, context => LinneaCards.DamageAll(context, Owner, Amount));
    }
}
