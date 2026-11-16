namespace LinneaMod.Content.Cards;

/// <summary>野途相伴：按原案与升级表实现。</summary>
[RegisterCard(typeof(LinneaCardPool))]
public sealed class TrailCompanions : LinneaCardBase
{
    protected override IEnumerable<IHoverTip> AdditionalHoverTips => new IHoverTip[] { HoverTipFactory.FromCard<Lumi>() };
    protected override IEnumerable<DynamicVar> CanonicalVars => new DynamicVar[] { new CardsVar(2), new DynamicVar("Encourage", 4m) };
    public override IEnumerable<CardKeyword> CanonicalKeywords
    {
        get
        {
            HashSet<CardKeyword> result = new() {  };
            LinneaKeywords.AddTo(result, LinneaKeywords.Encourage);
            return result;
        }
    }
    public override CardMultiplayerConstraint MultiplayerConstraint => CardMultiplayerConstraint.MultiplayerOnly;
    public TrailCompanions() : base(1, CardType.Skill, CardRarity.Uncommon, TargetType.AnyAlly) { }
    protected override async Task OnPlay(PlayerChoiceContext context, CardPlay play)
    {

        if (play.Target?.Player is not { } ally) return;
        await CardPileCmd.Draw(context, DynamicVars.Cards.BaseValue, ally);
        if (LumiCompanion.Find(Owner)?.Pile?.Type == PileType.Hand)
            await LumiCompanion.Encourage(context, Owner, DynamicVars["Encourage"].BaseValue, this);
        else await LumiCompanion.Recall(context, Owner);
    }
    protected override void OnUpgrade()
    {
        DynamicVars["Cards"].UpgradeValueBy(1m);
        DynamicVars["Encourage"].UpgradeValueBy(2m);
    }
}
