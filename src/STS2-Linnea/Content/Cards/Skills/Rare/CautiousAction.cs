namespace LinneaMod.Content.Cards;

/// <summary>谨慎行事：按原案与升级表实现。</summary>
[RegisterCard(typeof(LinneaCardPool))]
public sealed class CautiousAction : LinneaCardBase
{
    protected override IEnumerable<DynamicVar> CanonicalVars => new DynamicVar[] { new BlockVar(13m, ValueProp.Move) };
    public override bool GainsBlock => true;
    public override IEnumerable<CardKeyword> CanonicalKeywords
    {
        get
        {
            HashSet<CardKeyword> result = new() { CardKeyword.Innate, CardKeyword.Retain, CardKeyword.Exhaust };
            return result;
        }
    }

    public CautiousAction() : base(1, CardType.Skill, CardRarity.Rare, TargetType.Self) { }
    protected override async Task OnPlay(PlayerChoiceContext context, CardPlay play)
    {
        await CreatureCmd.GainBlock(Owner.Creature, DynamicVars.Block, play);
    }
    protected override void OnUpgrade()
    {
        DynamicVars["Block"].UpgradeValueBy(3m);
    }
}
