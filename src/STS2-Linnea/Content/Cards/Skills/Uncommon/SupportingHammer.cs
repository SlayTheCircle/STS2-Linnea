namespace LinneaMod.Content.Cards;

/// <summary>援护重锤：按原案与升级表实现。</summary>
[RegisterCard(typeof(LinneaCardPool))]
public sealed class SupportingHammer : LinneaCardBase
{
    protected override IEnumerable<IHoverTip> AdditionalHoverTips => new IHoverTip[] { HoverTipFactory.FromCard<Lumi>() };
    protected override IEnumerable<DynamicVar> CanonicalVars => new DynamicVar[] { new DynamicVar("Encourage", 6m), new DynamicVar("RecallEncourage", 9m) };
    public override IEnumerable<CardKeyword> CanonicalKeywords
    {
        get
        {
            HashSet<CardKeyword> result = new() {  };
            LinneaKeywords.AddTo(result, LinneaKeywords.Encourage);
            return result;
        }
    }

    public SupportingHammer() : base(1, CardType.Skill, CardRarity.Uncommon, TargetType.AnyEnemy) { }
    protected override async Task OnPlay(PlayerChoiceContext context, CardPlay play)
    {
        ArgumentNullException.ThrowIfNull(play.Target);

        if (LumiCompanion.Find(Owner)?.Pile?.Type == PileType.Hand)
        {
            await LumiCompanion.Encourage(context, Owner, DynamicVars["Encourage"].BaseValue, this);
            await LumiCompanion.Play(context, Owner, play.Target);
        }
        else
        {
            await LumiCompanion.Recall(context, Owner);
            await LumiCompanion.Encourage(context, Owner, DynamicVars["RecallEncourage"].BaseValue, this);
        }
    }
    protected override void OnUpgrade()
    {
        DynamicVars["Encourage"].UpgradeValueBy(2m);
        DynamicVars["RecallEncourage"].UpgradeValueBy(1m);
    }
}
