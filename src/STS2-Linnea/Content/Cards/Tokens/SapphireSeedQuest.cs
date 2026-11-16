using MegaCrit.Sts2.Core.Entities.RestSite;
using MegaCrit.Sts2.Core.Models.CardPools;
using LinneaMod.Content.RestSites;

namespace LinneaMod.Content.Cards;

[RegisterCard(typeof(QuestCardPool))]
public sealed class SapphireSeedQuest : LinneaCardBase
{
    public override int MaxUpgradeLevel => 0;
    public override IEnumerable<CardKeyword> CanonicalKeywords => new[] { CardKeyword.Unplayable };
    protected override IEnumerable<IHoverTip> AdditionalHoverTips => new[] { HoverTipFactory.FromCard<SapphireFlower>() };
    public SapphireSeedQuest() : base(-1, CardType.Quest, CardRarity.Quest, TargetType.None) { }
    public override bool TryModifyRestSiteOptions(Player player, ICollection<RestSiteOption> options)
    {
        if (player != Owner || options.Any(option => option is CultivateRestSiteOption)) return false;
        options.Add(new CultivateRestSiteOption(player));
        return true;
    }
    protected override Task OnPlay(PlayerChoiceContext context, CardPlay play) => Task.CompletedTask;
}
