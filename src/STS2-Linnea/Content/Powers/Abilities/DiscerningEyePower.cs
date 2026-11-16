namespace LinneaMod.Content.Powers;

[RegisterPower]
public sealed class DiscerningEyePower : LinneaPowerBase
{
    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Single;
    public override PowerInstanceType InstanceType => PowerInstanceType.Instanced;


    private int _triggeredTurn = -1;
    public override async Task AfterCardDrawn(PlayerChoiceContext context, CardModel card, bool fromHandDraw)
    {
        if (card.Owner.Creature != Owner || (card.Type != CardType.Status && !LinneaCards.IsColorless(card))
            || _triggeredTurn == card.Owner.PlayerCombatState!.TurnNumber) return;
        _triggeredTurn = card.Owner.PlayerCombatState!.TurnNumber;
        var hand = LinneaCards.Hand(card.Owner);
        bool exhausted = false;
        if (hand.Length > 0)
        {
            var prefs = new CardSelectorPrefs(new LocString("card_selection", "STS2_LINNEA_DISCERNING_EYE"), 1);
            foreach (var selected in await CardSelectCmd.FromSimpleGrid(context, hand, card.Owner, prefs))
                if (selected.Pile?.Type == PileType.Hand)
                {
                    await CardCmd.Exhaust(context, selected);
                    exhausted = true;
                }
        }
        if (exhausted) await CardPileCmd.Draw(context, Amount, card.Owner);
    }
}
