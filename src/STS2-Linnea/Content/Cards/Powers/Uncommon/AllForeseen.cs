namespace LinneaMod.Content.Cards;

/// <summary>料事如神：按原案与升级表实现。</summary>
[RegisterCard(typeof(LinneaCardPool))]
public sealed class AllForeseen : LinneaCardBase
{
    protected override IEnumerable<DynamicVar> CanonicalVars => new DynamicVar[] { new DynamicVar("Spend", 5m) };
    public override IEnumerable<CardKeyword> CanonicalKeywords
    {
        get
        {
            HashSet<CardKeyword> result = new() {  };
            LinneaKeywords.AddTo(result, LinneaKeywords.Foreknowledge);
            return result;
        }
    }
    protected override IEnumerable<IHoverTip> AdditionalHoverTips => new[] { HoverTipFactory.FromPower<AllForeseenPower>(DynamicVars["Spend"].IntValue) };
    public AllForeseen() : base(1, CardType.Power, CardRarity.Uncommon, TargetType.Self) { }
    protected override async Task OnPlay(PlayerChoiceContext context, CardPlay play)
    {
        await PowerCmd.Apply<AllForeseenPower>(context, Owner.Creature, DynamicVars["Spend"].BaseValue, Owner.Creature, this);
    }
    protected override void OnUpgrade()
    {
        DynamicVars["Spend"].UpgradeValueBy(-1m);
    }
}
