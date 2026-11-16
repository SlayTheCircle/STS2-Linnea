namespace LinneaMod.Content.Powers;

[RegisterPower]
public sealed class WorthEveryStonePower : LinneaPowerBase
{
    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Counter;


    public override async Task AfterCardChangedPiles(CardModel card, PileType oldPileType, AbstractModel? clonedBy)
    {
        if (card.Owner.Creature != Owner || card.Type != CardType.Status || oldPileType != PileType.Hand || card.Pile?.Type == PileType.Hand) return;
        await LinneaChoiceDispatch.Run(this, card.Owner, context => LinneaCards.DamageRandom(context, Owner, Amount));
    }
}
