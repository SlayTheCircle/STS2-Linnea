namespace LinneaMod.Content.Cards;

[RegisterCard(typeof(LinneaCardPool))]
public sealed class Ore : LinneaCardBase
{
    public override int MaxUpgradeLevel => 0;
    public override CardPoolModel VisualCardPool => ModelDb.CardPool<MegaCrit.Sts2.Core.Models.CardPools.StatusCardPool>();
    public override IEnumerable<CardKeyword> CanonicalKeywords => new[] { CardKeyword.Unplayable, CardKeyword.Retain };
    public Ore() : base(0, CardType.Status, CardRarity.Token, TargetType.None) { }
    protected override Task OnPlay(PlayerChoiceContext context, CardPlay play) => Task.CompletedTask;
}
