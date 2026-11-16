namespace LinneaMod.Content.Powers;

[RegisterPower]
public sealed class KnowYourEnemyPower : NextTurnPowerBase
{

    public override decimal ModifyHpLostAfterOsty(Creature target, decimal amount, ValueProp props, Creature? dealer, CardModel? cardSource)
        => target == Owner && IsDue ? amount / 2m : amount;
}
