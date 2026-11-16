namespace LinneaMod.Content.Cards;

/// <summary>稍纵即逝：按原案与升级表实现。</summary>
[RegisterCard(typeof(LinneaCardPool))]
public sealed class FleetingMoment : LinneaCardBase
{
    protected override IEnumerable<DynamicVar> CanonicalVars => new DynamicVar[] { new DynamicVar("Foreknowledge", 9m), new DynamicVar("Damage", 11m), new DynamicVar("Spend", 7m) };
    public override IEnumerable<CardKeyword> CanonicalKeywords
    {
        get
        {
            HashSet<CardKeyword> result = new() { CardKeyword.Retain };
            LinneaKeywords.AddTo(result, LinneaKeywords.Foreknowledge);
            return result;
        }
    }

    public FleetingMoment() : base(2, CardType.Skill, CardRarity.Uncommon, TargetType.Self) { }
    protected override async Task OnPlay(PlayerChoiceContext context, CardPlay play)
    {

        await Foreknowledge.Gain(context, Owner, DynamicVars["Foreknowledge"].BaseValue, this);
        if (await PowerCmd.Apply<FleetingNextTurnPower>(context, Owner.Creature, DynamicVars["Damage"].BaseValue, Owner.Creature, this) is { } power)
            power.SetForeknowledgeCost(DynamicVars["Spend"].IntValue);
    }
    protected override void OnUpgrade()
    {
        DynamicVars["Foreknowledge"].UpgradeValueBy(4m);
        DynamicVars["Damage"].UpgradeValueBy(2m);
        DynamicVars["Spend"].UpgradeValueBy(-2m);
    }
}
