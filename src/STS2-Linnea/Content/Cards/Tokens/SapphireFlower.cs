using MegaCrit.Sts2.Core.Models.CardPools;

namespace LinneaMod.Content.Cards;

[RegisterCard(typeof(EventCardPool))]
public sealed class SapphireFlower : LinneaCardBase
{
    public override int MaxUpgradeLevel => 0;
    public override CardPoolModel VisualCardPool => ModelDb.CardPool<ColorlessCardPool>();
    public override IEnumerable<CardKeyword> CanonicalKeywords => new[] { CardKeyword.Retain };
    protected override IEnumerable<DynamicVar> CanonicalVars => new[] { new DynamicVar("Energy", 2m) };
    public SapphireFlower() : base(0, CardType.Skill, CardRarity.Common, TargetType.Self) { }
    protected override async Task OnPlay(PlayerChoiceContext context, CardPlay play)
        => await PlayerCmd.GainEnergy(DynamicVars["Energy"].BaseValue, Owner);
}
