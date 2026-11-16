namespace LinneaMod.Content.Powers;

[RegisterPower]
public sealed class BlessingInDisguisePower : NextTurnPowerBase
{

    public override bool TryModifyPowerAmountReceived(PowerModel canonicalPower, Creature target, decimal amount, Creature? applier, out decimal modifiedAmount)
    {
        modifiedAmount = amount;
        if (!IsDue || target != Owner || amount == 0 || !canonicalPower.IsVisible
            || canonicalPower.GetTypeForAmount(amount) != PowerType.Debuff
            || Owner.HasPower<ArtifactPower>() || Foreknowledge.Amount(Owner.Player!) < 3) return false;
        modifiedAmount = 0;
        return true;
    }

    public override async Task AfterModifyingPowerAmountReceived(PowerModel power)
        => await LinneaChoiceDispatch.Run(this, Owner.Player!, async context =>
        {
            await Foreknowledge.Spend(context, Owner.Player!, 3);
            await CardPileCmd.Draw(context, Amount, Owner.Player!);
        });
}
