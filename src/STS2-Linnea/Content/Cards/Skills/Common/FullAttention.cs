namespace LinneaMod.Content.Cards;

/// <summary>全神贯注：按原案与升级表实现。</summary>
[RegisterCard(typeof(LinneaCardPool))]
public sealed class FullAttention : LinneaCardBase
{
    protected override IEnumerable<DynamicVar> CanonicalVars => new DynamicVar[] { new CardsVar(1), new DynamicVar("ExtraCards", 2m) };

    public FullAttention() : base(0, CardType.Skill, CardRarity.Common, TargetType.Self) { }
    protected override async Task OnPlay(PlayerChoiceContext context, CardPlay play)
    {

        var drawn = (await CardPileCmd.Draw(context, DynamicVars.Cards.BaseValue, Owner)).ToArray();
        if (drawn.Any(c => c.Type == CardType.Skill)) await CardPileCmd.Draw(context, DynamicVars["ExtraCards"].BaseValue, Owner);
    }
    protected override void OnUpgrade()
    {
        AddKeyword(CardKeyword.Retain);
    }
}
