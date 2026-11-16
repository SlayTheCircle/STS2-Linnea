namespace LinneaMod.Content.Cards;

/// <summary>超越命运：按原案与升级表实现。</summary>
[RegisterCard(typeof(LinneaCardPool))]
public sealed class BeyondFate : LinneaCardBase
{
    protected override IEnumerable<DynamicVar> CanonicalVars => new DynamicVar[] { new DynamicVar("Foreknowledge", 15m) };
    public override IEnumerable<CardKeyword> CanonicalKeywords
    {
        get
        {
            HashSet<CardKeyword> result = new() { CardKeyword.Exhaust };
            LinneaKeywords.AddTo(result, LinneaKeywords.Foreknowledge);
            return result;
        }
    }

    public BeyondFate() : base(0, CardType.Skill, CardRarity.Rare, TargetType.Self) { }
    protected override async Task OnPlay(PlayerChoiceContext context, CardPlay play)
    {

        await Foreknowledge.Gain(context, Owner, DynamicVars["Foreknowledge"].BaseValue, this);
        var known = PileType.Draw.GetPile(Owner).Cards.Take(Foreknowledge.Amount(Owner)).ToArray();
        if (known.Length == 0) return;
        var prefs = new CardSelectorPrefs(SelectionScreenPrompt, 0, known.Length) { RequireManualConfirmation = true };
        var selected = await CardSelectCmd.FromSimpleGrid(context, known, Owner, prefs);
        await CardPileCmd.Add(selected.Where(c => c.Pile?.Type == PileType.Draw), PileType.Discard);
    }
    protected override void OnUpgrade()
    {
        AddKeyword(CardKeyword.Retain);
    }
}
