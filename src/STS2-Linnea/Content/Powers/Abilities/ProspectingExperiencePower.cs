namespace LinneaMod.Content.Powers;

[RegisterPower]
public sealed class ProspectingExperiencePower : LinneaPowerBase
{
    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Counter;


    public override async Task AfterCardChangedPiles(CardModel card, PileType oldPileType, AbstractModel? clonedBy)
    {
        if (card.Owner.Creature != Owner || card.Type != CardType.Status || card.Pile?.Type != PileType.Discard || oldPileType == PileType.Discard) return;
        await LinneaChoiceDispatch.Run(this, card.Owner, context => Foreknowledge.Gain(context, card.Owner, Amount, null));
    }
}
