namespace LinneaMod.Content.Powers;

[RegisterPower]
public sealed class SpokenOmenPower : LinneaPowerBase
{
    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Single;


    protected override IEnumerable<DynamicVar> CanonicalVars => new[] { new DynamicVar("Block", 0m) };
    public int Block { get => DynamicVars["Block"].IntValue; set => DynamicVars["Block"].BaseValue = value; }
    public override PowerInstanceType InstanceType => PowerInstanceType.Instanced;
    public override async Task AfterPlayerTurnStart(PlayerChoiceContext context, Player player)
    {
        if (player.Creature != Owner) return;
        if (LinneaCards.AnyAttacking(Owner.CombatState!))
            await PowerCmd.Apply<OmenDamagePower>(context, Owner, Amount, Owner, null);
        else await PowerCmd.Apply<BlockNextTurnPower>(context, Owner, Block, Owner, null);
    }
}
