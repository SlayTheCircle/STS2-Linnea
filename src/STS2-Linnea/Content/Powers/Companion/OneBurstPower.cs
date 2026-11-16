namespace LinneaMod.Content.Powers;

[RegisterPower]
public sealed class OneBurstPower : LinneaPowerBase
{
    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Single;
    public override PowerInstanceType InstanceType => PowerInstanceType.Instanced;
    public override async Task AfterCardChangedPiles(CardModel card, PileType oldPileType, AbstractModel? clonedBy)
    {
        if (card.Owner.Creature != Owner || card is not Lumi lumi || card.Pile?.Type != PileType.Hand || oldPileType == PileType.Hand) return;
        await PowerCmd.Remove(this);
        if (!lumi.EnabledThisTurn)
        {
            if (Owner.GetPower<EncouragementPower>() is { } encouragement)
                await PowerCmd.Apply<EncouragementPower>(new ThrowingPlayerChoiceContext(), Owner, -Math.Min(Amount, encouragement.Amount), Owner, null);
            lumi.EnableForTurn();
        }
    }
}
