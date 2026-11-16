namespace LinneaMod.Content.Relics;

[RegisterRelic(typeof(LinneaRelicPool))]
public sealed class PetBackpack : LinneaRelicBase
{
    public override RelicRarity Rarity => RelicRarity.Uncommon;

    protected override IEnumerable<IHoverTip> AdditionalHoverTips => new[] { HoverTipFactory.FromCard<Lumi>() };
    protected override IEnumerable<string> RegisteredKeywordIds => new[] { LinneaKeywords.Encourage };
    public override async Task AfterCardPlayed(PlayerChoiceContext context, CardPlay play)
    {
        if (play.Card.Owner != Owner || play.Card is not Lumi) return;
        Flash();
        await CreatureCmd.GainBlock(Owner.Creature, 6, ValueProp.Unpowered, null);
        await LumiCompanion.Encourage(context, Owner, 2, null);
    }
}
