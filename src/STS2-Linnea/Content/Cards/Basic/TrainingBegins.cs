namespace LinneaMod.Content.Cards;

/// <summary>特训开始！：按原案与升级表实现。</summary>
[RegisterCard(typeof(LinneaCardPool))]
public sealed class TrainingBegins : LinneaCardBase
{
    protected override IEnumerable<DynamicVar> CanonicalVars => new DynamicVar[] { new DynamicVar("Encourage", 8m) };
    public override IEnumerable<CardKeyword> CanonicalKeywords
    {
        get
        {
            HashSet<CardKeyword> result = new() {  };
            LinneaKeywords.AddTo(result, LinneaKeywords.Encourage);
            return result;
        }
    }

    public TrainingBegins() : base(1, CardType.Skill, CardRarity.Basic, TargetType.Self) { }
    protected override async Task OnPlay(PlayerChoiceContext context, CardPlay play)
    {
        await LumiCompanion.Encourage(context, Owner, DynamicVars["Encourage"].BaseValue, this);
    }
    protected override void OnUpgrade()
    {
        DynamicVars["Encourage"].UpgradeValueBy(2m);
    }
}
