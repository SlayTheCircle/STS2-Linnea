namespace LinneaMod.Content.RestSites;

public sealed class CultivateRestSiteOption : LinneaRestSiteOptionBase
{
    public override string OptionId => "STS2_LINNEA_CULTIVATE";
    public override LocString? CustomTitle => new("events", "STS2_LINNEA_CULTIVATE.title");
    public override LocString Description => new("events", "STS2_LINNEA_CULTIVATE.description");
    public CultivateRestSiteOption(Player owner) : base(owner) { }
    public override async Task<bool> OnSelect()
    {
        var seeds = Owner.Deck.Cards.OfType<SapphireSeedQuest>().Cast<CardModel>().ToArray();
        if (seeds.Length == 0) return false;
        CardModel? seed = seeds[0];
        if (seeds.Length > 1)
        {
            var prefs = new CardSelectorPrefs(new LocString("card_selection", "STS2_LINNEA_CULTIVATE_SEED"), 1);
            seed = (await CardSelectCmd.FromSimpleGrid(new BlockingPlayerChoiceContext(), seeds, Owner, prefs)).FirstOrDefault();
        }
        if (seed?.Pile?.Type != PileType.Deck) return false;
        await CardPileCmd.RemoveFromDeck(seed);
        await CardPileCmd.Add(Owner.RunState.CreateCard<SapphireFlower>(Owner), Owner.Deck);
        Owner.RunState.CurrentMapPointHistoryEntry?.GetEntry(Owner.NetId).CompletedQuests.Add(seed.Id);
        return true;
    }
}
