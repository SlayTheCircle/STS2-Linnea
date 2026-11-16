namespace LinneaMod.Content.Powers;

[RegisterPower]
public sealed class BoldSpeculationPower : NextTurnPowerBase
{
    private bool _lostHp;
    public override Task AfterCurrentHpChanged(Creature creature, decimal delta)
    {
        if (creature == Owner && delta < 0) _lostHp = true;
        return Task.CompletedTask;
    }
    public override async Task AfterPlayerTurnStart(PlayerChoiceContext context, Player player)
    {
        if (player.Creature != Owner || !IsDue) return;
        await PowerCmd.Remove(this);
        if (!_lostHp)
        {
            await LinneaCards.DamageAll(context, Owner, Amount);
            await CardPileCmd.Draw(context, 1, player);
        }
        else
        {
            await PowerCmd.Apply<WeakPower>(context, Owner, 2, Owner, null);
            await CardCmd.Discard(context, await CardSelectCmd.FromHandForDiscard(context, player,
                new CardSelectorPrefs(CardSelectorPrefs.DiscardSelectionPrompt, 1), null, this));
        }
    }
}
