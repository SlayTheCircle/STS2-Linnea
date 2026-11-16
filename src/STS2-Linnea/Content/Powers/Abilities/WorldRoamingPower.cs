namespace LinneaMod.Content.Powers;

[RegisterPower]
public sealed class WorldRoamingPower : LinneaPowerBase
{
    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Single;
    public override PowerInstanceType InstanceType => PowerInstanceType.Instanced;


    private int _turn = -1;
    public override async Task AfterCardPlayed(PlayerChoiceContext context, CardPlay play)
    {
        if (play.Card is not Lumi lumi || lumi.Owner.Creature != Owner || _turn == lumi.Owner.PlayerCombatState!.TurnNumber) return;
        _turn = lumi.Owner.PlayerCombatState!.TurnNumber;
        foreach (var player in Owner.CombatState!.Players)
        {
            await CreatureCmd.GainBlock(player.Creature, Amount, ValueProp.Unpowered, null);
            if (lumi.DealtUnblockedDamage) await CardPileCmd.Draw(context, 1, player);
        }
    }
}
