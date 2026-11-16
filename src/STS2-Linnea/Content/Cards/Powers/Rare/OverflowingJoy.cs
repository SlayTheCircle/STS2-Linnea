namespace LinneaMod.Content.Cards;

/// <summary>幸福洋溢：按原案与升级表实现。</summary>
[RegisterCard(typeof(LinneaCardPool))]
public sealed class OverflowingJoy : LinneaCardBase
{
    protected override IEnumerable<DynamicVar> CanonicalVars => new DynamicVar[] { new DynamicVar("Damage", 2m) };
    protected override IEnumerable<IHoverTip> AdditionalHoverTips => new[] { HoverTipFactory.FromPower<OverflowingJoyPower>(DynamicVars["Damage"].IntValue) };
    public OverflowingJoy() : base(1, CardType.Power, CardRarity.Rare, TargetType.Self) { }
    protected override async Task OnPlay(PlayerChoiceContext context, CardPlay play)
    {
        await PowerCmd.Apply<OverflowingJoyPower>(context, Owner.Creature, DynamicVars["Damage"].BaseValue, Owner.Creature, this);
    }
    protected override void OnUpgrade()
    {
        AddKeyword(CardKeyword.Retain);
    }
}
