namespace LinneaMod.Content.Cards;

/// <summary>精挑细选：按原案与升级表实现。</summary>
[RegisterCard(typeof(LinneaCardPool))]
public sealed class FineSelection : LinneaCardBase
{
    protected override IEnumerable<IHoverTip> AdditionalHoverTips => new IHoverTip[] { HoverTipFactory.FromCard<Ore>() };
    protected override IEnumerable<DynamicVar> CanonicalVars => new DynamicVar[] { new BlockVar(7m, ValueProp.Move) };
    public override bool GainsBlock => true;

    public FineSelection() : base(1, CardType.Skill, CardRarity.Uncommon, TargetType.Self) { }
    protected override async Task OnPlay(PlayerChoiceContext context, CardPlay play)
    {
        await CreatureCmd.GainBlock(Owner.Creature, DynamicVars.Block, play);
        var top = PileType.Draw.GetPile(Owner).Cards.FirstOrDefault();
        if (top is not null) await CardCmd.Exhaust(context, top);
        await LinneaCards.Ore(Owner, PileType.Hand, 2);
        if (top?.Type == CardType.Status) await CardPileCmd.Draw(context, 2, Owner);
    }
    protected override void OnUpgrade()
    {
        DynamicVars["Block"].UpgradeValueBy(3m);
    }
}
