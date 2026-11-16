namespace LinneaMod.Content.Cards;

/// <summary>慧眼识珠：按原案与升级表实现。</summary>
[RegisterCard(typeof(LinneaCardPool))]
public sealed class DiscerningEye : LinneaCardBase
{
    protected override IEnumerable<DynamicVar> CanonicalVars => new DynamicVar[] { new CardsVar(2) };
    protected override IEnumerable<IHoverTip> AdditionalHoverTips => new[] { HoverTipFactory.FromPower<DiscerningEyePower>(DynamicVars["Cards"].IntValue) };
    public DiscerningEye() : base(2, CardType.Power, CardRarity.Uncommon, TargetType.Self) { }
    protected override async Task OnPlay(PlayerChoiceContext context, CardPlay play)
    {
        await PowerCmd.Apply<DiscerningEyePower>(context, Owner.Creature, DynamicVars["Cards"].BaseValue, Owner.Creature, this);
    }
    protected override void OnUpgrade()
    {
        EnergyCost.UpgradeBy(-1);
    }
}
