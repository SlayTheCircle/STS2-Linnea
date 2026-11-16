namespace LinneaMod.Content.Cards;

/// <summary>皆大欢喜！：按原案与升级表实现。</summary>
[RegisterCard(typeof(LinneaCardPool))]
[RegisterDustyTomeCard(typeof(Characters.Linnea))]
public sealed class HappyTogether : LinneaCardBase
{
    protected override IEnumerable<DynamicVar> CanonicalVars => new DynamicVar[] { new DynamicVar("Block", 3m) };
    public override IEnumerable<CardKeyword> CanonicalKeywords
    {
        get
        {
            HashSet<CardKeyword> result = new() { CardKeyword.Ethereal };
            return result;
        }
    }
    protected override IEnumerable<IHoverTip> AdditionalHoverTips => new[] { HoverTipFactory.FromPower<HappyTogetherPower>(DynamicVars["Block"].IntValue) };
    public HappyTogether() : base(2, CardType.Power, CardRarity.Ancient, TargetType.Self) { }
    protected override async Task OnPlay(PlayerChoiceContext context, CardPlay play)
    {
        await PowerCmd.Apply<HappyTogetherPower>(context, Owner.Creature, DynamicVars["Block"].BaseValue, Owner.Creature, this);
    }
    protected override void OnUpgrade()
    {
        DynamicVars["Block"].UpgradeValueBy(2m);
    }
}
