namespace LinneaMod.Content.Cards;

/// <summary>勘探经验：按原案与升级表实现。</summary>
[RegisterCard(typeof(LinneaCardPool))]
public sealed class ProspectingExperience : LinneaCardBase
{
    protected override IEnumerable<DynamicVar> CanonicalVars => new DynamicVar[] { new DynamicVar("Foreknowledge", 3m) };
    public override IEnumerable<CardKeyword> CanonicalKeywords
    {
        get
        {
            HashSet<CardKeyword> result = new() {  };
            LinneaKeywords.AddTo(result, LinneaKeywords.Foreknowledge);
            return result;
        }
    }
    protected override IEnumerable<IHoverTip> AdditionalHoverTips => new[] { HoverTipFactory.FromPower<ProspectingExperiencePower>(DynamicVars["Foreknowledge"].IntValue) };
    public ProspectingExperience() : base(1, CardType.Power, CardRarity.Uncommon, TargetType.Self) { }
    protected override async Task OnPlay(PlayerChoiceContext context, CardPlay play)
    {
        await PowerCmd.Apply<ProspectingExperiencePower>(context, Owner.Creature, DynamicVars["Foreknowledge"].BaseValue, Owner.Creature, this);
    }
    protected override void OnUpgrade()
    {
        DynamicVars["Foreknowledge"].UpgradeValueBy(2m);
    }
}
