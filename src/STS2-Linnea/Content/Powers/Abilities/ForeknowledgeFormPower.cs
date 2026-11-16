namespace LinneaMod.Content.Powers;

[RegisterPower]
public sealed class ForeknowledgeFormPower : LinneaPowerBase
{
    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Counter;


    public override async Task AfterCardPlayed(PlayerChoiceContext context, CardPlay play)
    {
        if (play.Card.Owner.Creature != Owner) return;
        var first = CombatManager.Instance.History.CardPlaysStarted.FirstOrDefault(e =>
            e.Actor == Owner && e.HappenedThisTurn(Owner.CombatState));
        if (first?.CardPlay != play) return;
        Player player = play.Card.Owner;
        await Foreknowledge.Gain(context, player, Amount, play.Card);
        int current = Foreknowledge.Amount(player);
        int lost = await Foreknowledge.Spend(context, player, current - current / 2);
        await CreatureCmd.Heal(Owner, lost);
    }
}
