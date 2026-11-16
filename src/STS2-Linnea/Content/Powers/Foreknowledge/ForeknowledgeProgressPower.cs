namespace LinneaMod.Content.Powers;

/// <summary>Cumulative gains survive spending the last visible stack; scoped to this combat.</summary>
[RegisterPower]
public sealed class ForeknowledgeProgressPower : PowerModel
{
    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Counter;
    protected override bool IsVisibleInternal => false;
}
