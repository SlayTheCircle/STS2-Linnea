namespace LinneaMod.Content.Cards;

/// <summary>精心雕琢：按原案与升级表实现。</summary>
[RegisterCard(typeof(LinneaCardPool))]
public sealed class CarefulCarving : LinneaCardBase
{
    protected override IEnumerable<DynamicVar> CanonicalVars => new DynamicVar[] { new CardsVar(3), new DamageVar(9m, ValueProp.Move), new BlockVar(3m, ValueProp.Move) };
    public override bool GainsBlock => true;

    public CarefulCarving() : base(1, CardType.Attack, CardRarity.Uncommon, TargetType.AllEnemies) { }
    protected override async Task OnPlay(PlayerChoiceContext context, CardPlay play)
    {

        var drawn = (await CardPileCmd.Draw(context, DynamicVars.Cards.BaseValue, Owner)).ToArray();
        foreach (var card in drawn.Where(c => c.Pile?.Type == PileType.Hand))
        {
            await CardCmd.Discard(context, card);
            if (card.Type == CardType.Status)
            {
        await DamageCmd.Attack(DynamicVars.Damage.BaseValue).FromCard(this, play).TargetingAllOpponents(CombatState!).WithHitFx("vfx/vfx_attack_slash").Execute(context);
            }
            else
            {
        await CreatureCmd.GainBlock(Owner.Creature, DynamicVars.Block, play);
            }
        }
    }
    protected override void OnUpgrade()
    {
        DynamicVars["Damage"].UpgradeValueBy(1m);
        DynamicVars["Block"].UpgradeValueBy(1m);
    }
}
