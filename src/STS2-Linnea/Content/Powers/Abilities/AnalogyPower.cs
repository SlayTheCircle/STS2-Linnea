namespace LinneaMod.Content.Powers;

[RegisterPower]
public sealed class AnalogyPower : LinneaPowerBase
{
    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Counter;


    public override async Task AfterShuffle(PlayerChoiceContext context, Player player)
    {
        if (player.Creature == Owner) await LinneaCards.DamageAll(context, Owner, Amount * PileType.Draw.GetPile(player).Cards.Count);
    }
}
