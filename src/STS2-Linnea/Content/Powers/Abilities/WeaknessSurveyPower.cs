namespace LinneaMod.Content.Powers;

[RegisterPower]
public sealed class WeaknessSurveyPower : LinneaPowerBase
{
    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Counter;


}
