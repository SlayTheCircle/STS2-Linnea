namespace LinneaMod.Content.Powers;

[RegisterPower]
public sealed class LightningNervesPower : LinneaPowerBase
{
    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Single;
    public override PowerInstanceType InstanceType => PowerInstanceType.Instanced;

    public override async Task AfterCardExhausted(PlayerChoiceContext context, CardModel card, bool causedByEthereal)
    {
        if (card.Owner.Creature != Owner) return;
        await PowerCmd.Remove(this);
        await CardPileCmd.Draw(context, Amount, card.Owner);
    }
}
