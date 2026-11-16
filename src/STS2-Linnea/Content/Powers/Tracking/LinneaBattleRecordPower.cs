namespace LinneaMod.Content.Powers;

[RegisterPower]
public sealed class LinneaBattleRecordPower : PowerModel
{
    private int _lastHpLossTurn = -1;
    public decimal Gold { get; set; }
    public bool UnharmedLastTurn => Owner.Player!.PlayerCombatState!.TurnNumber > 1
        && _lastHpLossTurn != Owner.Player.PlayerCombatState.TurnNumber - 1;
    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Single;
    protected override bool IsVisibleInternal => false;
    public override Task AfterCurrentHpChanged(Creature creature, decimal delta)
    {
        if (creature == Owner && delta < 0) _lastHpLossTurn = Owner.Player!.PlayerCombatState!.TurnNumber;
        return Task.CompletedTask;
    }
    public override Task AfterDamageGiven(PlayerChoiceContext context, Creature? dealer, DamageResult result, ValueProp props, Creature target, CardModel? cardSource)
    {
        if (dealer == Owner && cardSource is GoldInTheSand) Gold += result.TotalDamage;
        return Task.CompletedTask;
    }
    // Native successful combat teardown removes Powers before AfterCombatVictory.
    // AfterCombatEnd is its victory-only callback while this record still exists.
    public override async Task AfterCombatEnd(MegaCrit.Sts2.Core.Rooms.CombatRoom room)
    {
        decimal gold = Gold;
        Gold = 0;
        if (gold > 0) await PlayerCmd.GainGold(gold, Owner.Player!);
    }
}
