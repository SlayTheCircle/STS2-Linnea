namespace LinneaMod.Content.Cards;

/// <summary>生存技巧：按原案与升级表实现。</summary>
[RegisterCard(typeof(LinneaCardPool))]
public sealed class SurvivalSkills : LinneaCardBase
{

    public SurvivalSkills() : base(1, CardType.Skill, CardRarity.Common, TargetType.Self) { }
    protected override async Task OnPlay(PlayerChoiceContext context, CardPlay play)
    {

        var drawPile = PileType.Draw.GetPile(Owner).Cards.ToArray();
        if (drawPile.Length > 0)
        {
            var chosen = await CardSelectCmd.FromSimpleGrid(context, drawPile, Owner,
                new CardSelectorPrefs(new LocString("card_selection", "STS2_LINNEA_SURVIVAL_DISCARD"), 1));
            await CardPileCmd.Add(chosen.Where(c => c.Pile?.Type == PileType.Draw), PileType.Discard);
        }
        var discard = PileType.Discard.GetPile(Owner).Cards.ToArray();
        if (discard.Length > 0)
        {
            var chosen = await CardSelectCmd.FromSimpleGrid(context, discard, Owner,
                new CardSelectorPrefs(new LocString("card_selection", "STS2_LINNEA_SURVIVAL_TOP"), 1));
            await CardPileCmd.Add(chosen.Where(c => c.Pile?.Type == PileType.Discard), PileType.Draw, CardPilePosition.Top);
        }
    }
    protected override void OnUpgrade()
    {
        EnergyCost.UpgradeBy(-1);
    }
}
