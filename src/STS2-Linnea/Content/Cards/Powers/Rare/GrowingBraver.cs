namespace LinneaMod.Content.Cards;

/// <summary>愈战愈勇：按原案与升级表实现。</summary>
[RegisterCard(typeof(LinneaCardPool))]
public sealed class GrowingBraver : LinneaCardBase
{
    protected override IEnumerable<IHoverTip> AdditionalHoverTips => new[] { HoverTipFactory.FromPower<GrowingBraverPower>(1) };
    public GrowingBraver() : base(1, CardType.Power, CardRarity.Rare, TargetType.Self) { }
    protected override async Task OnPlay(PlayerChoiceContext context, CardPlay play)
    {
        await PowerCmd.Apply<GrowingBraverPower>(context, Owner.Creature, 1, Owner.Creature, this);
    }
    protected override void OnUpgrade()
    {
        AddKeyword(CardKeyword.Innate);
    }
}
