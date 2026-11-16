namespace LinneaMod.Content.Cards;

/// <summary>不安预感：按原案与升级表实现。</summary>
[RegisterCard(typeof(LinneaCardPool))]
public sealed class UneasyPremonition : LinneaCardBase
{
    protected override IEnumerable<DynamicVar> CanonicalVars => new DynamicVar[] { new DynamicVar("Heal", 8m), new BlockVar(9m, ValueProp.Move) };
    public override bool GainsBlock => true;
    public override IEnumerable<CardKeyword> CanonicalKeywords
    {
        get
        {
            HashSet<CardKeyword> result = new() { CardKeyword.Retain };
            return result;
        }
    }

    public UneasyPremonition() : base(3, CardType.Skill, CardRarity.Uncommon, TargetType.Self) { }
    protected override async Task OnPlay(PlayerChoiceContext context, CardPlay play)
    {

        if (PileType.Draw.GetPile(Owner).Cards.FirstOrDefault() is { Type: CardType.Status } top)
        {
            await CreatureCmd.Heal(Owner.Creature, DynamicVars["Heal"].BaseValue);
            await CardCmd.Exhaust(context, top);
            await PlayerCmd.GainEnergy(3, Owner);
        }
        else
        {
            await CardPileCmd.Draw(context, 1, Owner);
        await CreatureCmd.GainBlock(Owner.Creature, DynamicVars.Block, play);
            await PlayerCmd.GainEnergy(2, Owner);
        }
    }
    protected override void OnUpgrade()
    {
        EnergyCost.UpgradeBy(-1);
    }
}
