namespace LinneaMod.Content.Cards;

/// <summary>投喂矿石：按原案与升级表实现。</summary>
[RegisterCard(typeof(LinneaCardPool))]
public sealed class FeedTheOre : LinneaCardBase
{
    protected override IEnumerable<IHoverTip> AdditionalHoverTips => new IHoverTip[] { HoverTipFactory.FromCard<Lumi>() };
    protected override IEnumerable<DynamicVar> CanonicalVars => new DynamicVar[] { new DynamicVar("Encourage", 4m) };
    public override IEnumerable<CardKeyword> CanonicalKeywords
    {
        get
        {
            HashSet<CardKeyword> result = new() { CardKeyword.Exhaust };
            LinneaKeywords.AddTo(result, LinneaKeywords.Encourage);
            return result;
        }
    }

    public FeedTheOre() : base(2, CardType.Skill, CardRarity.Uncommon, TargetType.Self) { }
    protected override async Task OnPlay(PlayerChoiceContext context, CardPlay play)
    {

        var cards = LinneaCards.Hand(Owner).Where(c => c.Type is CardType.Status or CardType.Curse).ToArray();
        int exhausted = 0;
        foreach (var card in cards)
            if (card.Pile?.Type == PileType.Hand && await LinneaCardExhaust.Try(context, card)) exhausted++;
        await LumiCompanion.Encourage(context, Owner, exhausted * DynamicVars["Encourage"].BaseValue, this);
        await LumiCompanion.Recall(context, Owner);
    }
    protected override void OnUpgrade()
    {
        DynamicVars["Encourage"].UpgradeValueBy(2m);
    }
}
