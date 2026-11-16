namespace LinneaMod.Content.Cards;

/// <summary>暂避锋芒：按原案与升级表实现。</summary>
[RegisterCard(typeof(LinneaCardPool))]
public sealed class AvoidTheEdge : LinneaCardBase
{
    protected override IEnumerable<DynamicVar> CanonicalVars => new DynamicVar[] { new DynamicVar("CostForeknowledge", 4m), new DynamicVar("StatusForeknowledge", 3m) };
    public override IEnumerable<CardKeyword> CanonicalKeywords
    {
        get
        {
            HashSet<CardKeyword> result = new() {  };
            LinneaKeywords.AddTo(result, LinneaKeywords.Foreknowledge);
            return result;
        }
    }

    public AvoidTheEdge() : base(2, CardType.Skill, CardRarity.Uncommon, TargetType.Self) { }
    protected override async Task OnPlay(PlayerChoiceContext context, CardPlay play)
    {

        var selected = (await CardSelectCmd.FromHandForDiscard(context, Owner,
            new CardSelectorPrefs(CardSelectorPrefs.DiscardSelectionPrompt, Math.Min(3, LinneaCards.Hand(Owner).Length)), null, this)).ToArray();
        int costs = selected.Sum(c => c.EnergyCost.CostsX ? 0 : Math.Max(0, c.EnergyCost.GetWithModifiers(CostModifiers.All)));
        int statuses = selected.Count(c => c.Type == CardType.Status);
        await CardCmd.Discard(context, selected);
        await Foreknowledge.Gain(context, Owner, costs * DynamicVars["CostForeknowledge"].BaseValue + statuses * DynamicVars["StatusForeknowledge"].BaseValue, this);
        if (costs > 0) await PowerCmd.Apply<DrawCardsNextTurnPower>(context, Owner.Creature, costs, Owner.Creature, this);
        if (statuses > 0) await PowerCmd.Apply<EnergyNextTurnPower>(context, Owner.Creature, statuses, Owner.Creature, this);
    }
    protected override void OnUpgrade()
    {
        DynamicVars["CostForeknowledge"].UpgradeValueBy(2m);
        DynamicVars["StatusForeknowledge"].UpgradeValueBy(1m);
    }
}
