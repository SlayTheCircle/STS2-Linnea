namespace LinneaMod.Content.Powers;

[RegisterPower]
public sealed class ContinuousHammerPower : LinneaPowerBase
{
    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Single;


}
