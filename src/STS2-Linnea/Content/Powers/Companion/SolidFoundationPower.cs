namespace LinneaMod.Content.Powers;

[RegisterPower]
public sealed class SolidFoundationPower : LinneaPowerBase
{
    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Counter;

    public override async Task AfterBlockGained(Creature creature, decimal amount, ValueProp props, CardModel? source)
    {
        if (creature == Owner && amount > 0)
            await LumiCompanion.Encourage(new ThrowingPlayerChoiceContext(), Owner.Player!, Amount, source);
    }
}
