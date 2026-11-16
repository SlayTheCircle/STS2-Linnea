namespace LinneaMod.Content.Cards;

/// <summary>加速未来：按原案与升级表实现。</summary>
[RegisterCard(typeof(LinneaCardPool))]
public sealed class AccelerateTheFuture : LinneaCardBase
{
    protected override IEnumerable<DynamicVar> CanonicalVars => new DynamicVar[] { new DynamicVar("Foreknowledge", 9m), new CardsVar(3) };
    public override IEnumerable<CardKeyword> CanonicalKeywords
    {
        get
        {
            HashSet<CardKeyword> result = new() {  };
            LinneaKeywords.AddTo(result, LinneaKeywords.Foreknowledge);
            return result;
        }
    }

    public AccelerateTheFuture() : base(1, CardType.Skill, CardRarity.Uncommon, TargetType.Self) { }
    protected override async Task OnPlay(PlayerChoiceContext context, CardPlay play)
    {

        if (Foreknowledge.Amount(Owner) < 12)
            await Foreknowledge.Gain(context, Owner, DynamicVars["Foreknowledge"].BaseValue, this);
        else
        {
            var drawn = (await CardPileCmd.Draw(context, DynamicVars.Cards.BaseValue, Owner)).Where(c => c.Pile?.Type == PileType.Hand).ToArray();
            if (drawn.Length == 0) return;
            var prefs = new CardSelectorPrefs(SelectionScreenPrompt, 1, drawn.Length) { RequireManualConfirmation = true };
            await CardCmd.Discard(context, await CardSelectCmd.FromSimpleGrid(context, drawn, Owner, prefs));
        }
    }
    protected override void OnUpgrade()
    {
        DynamicVars["Foreknowledge"].UpgradeValueBy(3m);
        DynamicVars["Cards"].UpgradeValueBy(1m);
    }
}
