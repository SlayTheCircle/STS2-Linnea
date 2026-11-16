namespace LinneaMod.Content.Powers;

[RegisterPower]
public sealed class EncouragementPower : LinneaPowerBase
{
    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Counter;
    public override async Task AfterPowerAmountChanged(PlayerChoiceContext context, PowerModel power, decimal amount, Creature? applier, CardModel? cardSource)
    {
        if (power == this && amount > 0 && Owner.Player is { } player && LumiCompanion.Find(player) is null)
            await LumiCompanion.Generate(context, player);
    }

    public override async Task AfterSideTurnEnd(PlayerChoiceContext context, CombatSide side, IEnumerable<Creature> participants)
    {
        if (!participants.Contains(Owner) || Amount <= 0) return;
        Creature owner = Owner;
        int lost = Amount - Amount / 2;
        await PowerCmd.ModifyAmount(context, this, -lost, owner, null);
        await CreatureCmd.GainBlock(owner, lost, ValueProp.Unpowered, null);
    }

    protected override decimal ModifyDamageAdditiveCore(Creature? target, decimal amount, ValueProp props, Creature? dealer, CardModel? card)
        => dealer == Owner && card is Lumi && props.IsPoweredAttack() ? Amount : 0m;
}
