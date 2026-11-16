namespace LinneaMod.Content.Cards;

/// <summary>随行护卫：按原案与升级表实现。</summary>
[RegisterCard(typeof(LinneaCardPool))]
public sealed class TravelingGuard : LinneaCardBase
{
    protected override IEnumerable<DynamicVar> CanonicalVars => new DynamicVar[] { new BlockVar(7m, ValueProp.Move), new DynamicVar("ColorlessBlock", 4m), new DynamicVar("StatusBlock", 2m) };
    public override bool GainsBlock => true;

    public TravelingGuard() : base(1, CardType.Skill, CardRarity.Uncommon, TargetType.Self) { }
    protected override async Task OnPlay(PlayerChoiceContext context, CardPlay play)
    {
        await CreatureCmd.GainBlock(Owner.Creature, DynamicVars.Block, play);
        var hand = LinneaCards.Hand(Owner);
        decimal extra = hand.Count(LinneaCards.IsColorless) * DynamicVars["ColorlessBlock"].BaseValue
            + hand.Count(c => c.Type == CardType.Status) * DynamicVars["StatusBlock"].BaseValue;
        if (extra > 0) await CreatureCmd.GainBlock(Owner.Creature, extra, ValueProp.Move, play);
    }
    protected override void OnUpgrade()
    {
        DynamicVars["Block"].UpgradeValueBy(2m);
        DynamicVars["ColorlessBlock"].UpgradeValueBy(2m);
    }
}
