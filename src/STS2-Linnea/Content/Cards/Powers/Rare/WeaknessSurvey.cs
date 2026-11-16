namespace LinneaMod.Content.Cards;

/// <summary>弱点勘察：按原案与升级表实现。</summary>
[RegisterCard(typeof(LinneaCardPool))]
public sealed class WeaknessSurvey : LinneaCardBase
{
    protected override IEnumerable<DynamicVar> CanonicalVars => new DynamicVar[] { new DynamicVar("Vulnerable", 2m) };
    protected override IEnumerable<IHoverTip> AdditionalHoverTips => new[] { HoverTipFactory.FromPower<WeaknessSurveyPower>(DynamicVars["Vulnerable"].IntValue) };
    public WeaknessSurvey() : base(3, CardType.Power, CardRarity.Rare, TargetType.Self) { }
    protected override async Task OnPlay(PlayerChoiceContext context, CardPlay play)
    {
        await PowerCmd.Apply<WeaknessSurveyPower>(context, Owner.Creature, DynamicVars["Vulnerable"].BaseValue, Owner.Creature, this);
    }
    protected override void OnUpgrade()
    {
        EnergyCost.UpgradeBy(-1);
    }
}
