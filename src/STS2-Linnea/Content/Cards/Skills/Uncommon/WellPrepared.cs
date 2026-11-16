namespace LinneaMod.Content.Cards;

/// <summary>有备无患：按原案与升级表实现。</summary>
[RegisterCard(typeof(LinneaCardPool))]
public sealed class WellPrepared : LinneaCardBase
{
    protected override IEnumerable<DynamicVar> CanonicalVars => new DynamicVar[] { new DynamicVar("Foreknowledge", 3m), new DynamicVar("Threshold", 10m) };
    public override IEnumerable<CardKeyword> CanonicalKeywords
    {
        get
        {
            HashSet<CardKeyword> result = new() {  };
            LinneaKeywords.AddTo(result, LinneaKeywords.Foreknowledge);
            return result;
        }
    }
    public override CardMultiplayerConstraint MultiplayerConstraint => CardMultiplayerConstraint.MultiplayerOnly;
    public WellPrepared() : base(1, CardType.Skill, CardRarity.Uncommon, TargetType.AnyAlly) { }
    protected override async Task OnPlay(PlayerChoiceContext context, CardPlay play)
    {

        if (play.Target?.Player is not { } ally) return;
        await Foreknowledge.Gain(context, Owner, DynamicVars["Foreknowledge"].BaseValue, this);
        await PowerCmd.Apply<DrawCardsNextTurnPower>(context, ally.Creature, 2, Owner.Creature, this);
        if (Foreknowledge.Amount(Owner) >= DynamicVars["Threshold"].IntValue)
            await PowerCmd.Apply<EnergyNextTurnPower>(context, ally.Creature, 1, Owner.Creature, this);
    }
    protected override void OnUpgrade()
    {
        DynamicVars["Foreknowledge"].UpgradeValueBy(2m);
        DynamicVars["Threshold"].UpgradeValueBy(-2m);
    }
}
