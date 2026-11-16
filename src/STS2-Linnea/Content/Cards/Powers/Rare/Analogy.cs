namespace LinneaMod.Content.Cards;

/// <summary>类比分析：按原案与升级表实现。</summary>
[RegisterCard(typeof(LinneaCardPool))]
public sealed class Analogy : LinneaCardBase
{
    protected override IEnumerable<IHoverTip> AdditionalHoverTips => new[] { HoverTipFactory.FromPower<AnalogyPower>(1) };
    public Analogy() : base(1, CardType.Power, CardRarity.Rare, TargetType.Self) { }
    protected override async Task OnPlay(PlayerChoiceContext context, CardPlay play)
    {
        await PowerCmd.Apply<AnalogyPower>(context, Owner.Creature, 1, Owner.Creature, this);
    }
    protected override void OnUpgrade()
    {
        AddKeyword(CardKeyword.Innate);
    }
}
