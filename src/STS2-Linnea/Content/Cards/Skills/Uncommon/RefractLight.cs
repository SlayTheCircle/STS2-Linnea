namespace LinneaMod.Content.Cards;

/// <summary>折射光线：按原案与升级表实现。</summary>
[RegisterCard(typeof(LinneaCardPool))]
public sealed class RefractLight : LinneaCardBase
{
    protected override IEnumerable<IHoverTip> AdditionalHoverTips => new IHoverTip[] { HoverTipFactory.FromCard<Ore>() };
    public override IEnumerable<CardKeyword> CanonicalKeywords
    {
        get
        {
            HashSet<CardKeyword> result = new() { CardKeyword.Exhaust };
            return result;
        }
    }

    public RefractLight() : base(2, CardType.Skill, CardRarity.Uncommon, TargetType.Self) { }
    protected override async Task OnPlay(PlayerChoiceContext context, CardPlay play)
    {

        var cards = LinneaCards.Hand(Owner).Where(c => c.Type is CardType.Status or CardType.Curse).ToArray();
        int ores = 0;
        foreach (var card in cards)
            if (card.Pile?.Type == PileType.Hand)
            {
                if (await LinneaCardExhaust.Try(context, card) && card is Ore) ores++;
            }
        if (ores == 0 || LinneaCards.Hand(Owner).Length == 0) return;
        var selected = await CardSelectCmd.FromHand(context, Owner, new CardSelectorPrefs(SelectionScreenPrompt, 1), null, this);
        foreach (var card in selected) card.BaseReplayCount += ores;
    }
    protected override void OnUpgrade()
    {
        EnergyCost.UpgradeBy(-1);
    }
}
