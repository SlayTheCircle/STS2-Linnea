namespace LinneaMod.Content.Powers;

[RegisterPower]
public sealed class ForeknowledgePower : LinneaPowerBase
{
    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Counter;
    public override async Task AfterCardDrawn(PlayerChoiceContext context, CardModel card, bool fromHandDraw)
    {
        if (card.Owner.Creature == Owner && Amount > 0 && Owner.GetPower<MirageForeknowledgeGuardPower>() is null)
            await Foreknowledge.Spend(context, card.Owner, 1);
    }
}
