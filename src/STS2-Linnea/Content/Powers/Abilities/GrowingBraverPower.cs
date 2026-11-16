namespace LinneaMod.Content.Powers;

[RegisterPower]
public sealed class GrowingBraverPower : LinneaPowerBase
{
    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Single;


    public override async Task AfterCardExhausted(PlayerChoiceContext context, CardModel card, bool causedByEthereal)
    {
        if (card.Owner.Creature == Owner) await LumiCompanion.Recall(context, card.Owner);
    }
}
