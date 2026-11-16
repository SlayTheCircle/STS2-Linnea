namespace LinneaMod.Content.Powers;

[RegisterPower]
public sealed class VisionNextTurnPower : NextTurnPowerBase
{

    public override async Task AfterBlockGained(Creature creature, decimal amount, ValueProp props, CardModel? source)
    {
        if (creature != Owner || amount <= 0 || !IsDue) return;
        await PowerCmd.Remove(this);
        await LinneaChoiceDispatch.Run(this, Owner.Player!, context => Foreknowledge.Gain(context, Owner.Player!, Amount, source));
    }
}
