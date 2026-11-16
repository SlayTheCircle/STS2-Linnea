namespace LinneaMod.Content.Powers;

/// <summary>须臾幻像的抽牌预知保护；在拥有者下个回合开始前移除。</summary>
[RegisterPower]
public sealed class MirageForeknowledgeGuardPower : PowerModel
{
    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Single;
    protected override bool IsVisibleInternal => false;

    public override async Task BeforeSideTurnStart(PlayerChoiceContext choiceContext, CombatSide side, IReadOnlyList<Creature> participants, ICombatState combatState)
    {
        if (participants.Contains(Owner)) await PowerCmd.Remove(this);
    }
}
