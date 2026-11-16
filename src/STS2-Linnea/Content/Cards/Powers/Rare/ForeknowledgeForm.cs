namespace LinneaMod.Content.Cards;

/// <summary>预知形态：按原案与升级表实现。</summary>
[RegisterCard(typeof(LinneaCardPool))]
public sealed class ForeknowledgeForm : LinneaCardBase
{
    protected override IEnumerable<DynamicVar> CanonicalVars => new DynamicVar[] { new DynamicVar("Foreknowledge", 7m), new DynamicVar("InitialForeknowledge", 16m) };
    public override IEnumerable<CardKeyword> CanonicalKeywords
    {
        get
        {
            HashSet<CardKeyword> result = new() {  };
            LinneaKeywords.AddTo(result, LinneaKeywords.Foreknowledge);
            return result;
        }
    }
    protected override IEnumerable<IHoverTip> AdditionalHoverTips => new[] { HoverTipFactory.FromPower<ForeknowledgeFormPower>(DynamicVars["Foreknowledge"].IntValue) };
    public ForeknowledgeForm() : base(3, CardType.Power, CardRarity.Rare, TargetType.Self) { }
    protected override async Task OnPlay(PlayerChoiceContext context, CardPlay play)
    {
        await Foreknowledge.Gain(context, Owner, DynamicVars["InitialForeknowledge"].BaseValue, this);
        await PowerCmd.Apply<ForeknowledgeFormPower>(context, Owner.Creature, DynamicVars["Foreknowledge"].BaseValue, Owner.Creature, this);
    }
    protected override void OnUpgrade()
    {
        EnergyCost.UpgradeBy(-1);
        DynamicVars["Foreknowledge"].UpgradeValueBy(3m);
        DynamicVars["InitialForeknowledge"].UpgradeValueBy(4m);
    }
}
