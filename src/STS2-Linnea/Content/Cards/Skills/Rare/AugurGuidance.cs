namespace LinneaMod.Content.Cards;

/// <summary>谶鸟引航：按原案与升级表实现。</summary>
[RegisterCard(typeof(LinneaCardPool))]
public sealed class AugurGuidance : LinneaCardBase
{
    protected override IEnumerable<DynamicVar> CanonicalVars => new DynamicVar[] { new DynamicVar("Foreknowledge", 8m), new CardsVar(2) };
    public override IEnumerable<CardKeyword> CanonicalKeywords
    {
        get
        {
            HashSet<CardKeyword> result = new() { CardKeyword.Exhaust };
            LinneaKeywords.AddTo(result, LinneaKeywords.Foreknowledge);
            return result;
        }
    }
    public override CardMultiplayerConstraint MultiplayerConstraint => CardMultiplayerConstraint.MultiplayerOnly;
    public AugurGuidance() : base(0, CardType.Skill, CardRarity.Rare, TargetType.AnyAlly) { }
    protected override async Task OnPlay(PlayerChoiceContext context, CardPlay play)
    {

        if (play.Target?.Player is not { } ally) return;
        int spent = await Foreknowledge.Spend(context, Owner, DynamicVars["Foreknowledge"].IntValue);
        var known = PileType.Draw.GetPile(ally).Cards.Take(spent).ToArray();
        if (known.Length == 0) return;
        var prefs = new CardSelectorPrefs(SelectionScreenPrompt, 0, Math.Min(DynamicVars.Cards.IntValue, known.Length)) { RequireManualConfirmation = true };
        var selected = await CardSelectCmd.FromSimpleGrid(context, known, Owner, prefs);
        // Unselected cards are never moved, preserving their precise draw order.
        await CardPileCmd.Add(selected.Where(c => c.Pile?.Type == PileType.Draw), PileType.Hand);
    }
    protected override void OnUpgrade()
    {
        DynamicVars["Foreknowledge"].UpgradeValueBy(4m);
        DynamicVars["Cards"].UpgradeValueBy(1m);
    }
}
