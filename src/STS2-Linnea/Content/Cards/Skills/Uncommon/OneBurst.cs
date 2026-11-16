namespace LinneaMod.Content.Cards;

/// <summary>一鼓作气：按原案与升级表实现。</summary>
[RegisterCard(typeof(LinneaCardPool))]
public sealed class OneBurst : LinneaCardBase
{
    protected override IEnumerable<IHoverTip> AdditionalHoverTips => new IHoverTip[] { HoverTipFactory.FromCard<Lumi>() };
    protected override IEnumerable<DynamicVar> CanonicalVars => new DynamicVar[] { new CardsVar(2), new DynamicVar("Reduction", 5m) };

    public OneBurst() : base(1, CardType.Skill, CardRarity.Uncommon, TargetType.Self) { }
    protected override async Task OnPlay(PlayerChoiceContext context, CardPlay play)
    {
        await PowerCmd.Apply<OneBurstPower>(context, Owner.Creature, DynamicVars["Reduction"].BaseValue, Owner.Creature, this);
        await CardPileCmd.Draw(context, DynamicVars.Cards.BaseValue, Owner);
    }
    protected override void OnUpgrade()
    {
        DynamicVars["Cards"].UpgradeValueBy(1m);
        DynamicVars["Reduction"].UpgradeValueBy(-1m);
    }
}
