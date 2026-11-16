namespace LinneaMod.Content.Cards;

/// <summary>层层遴选：按原案与升级表实现。</summary>
[RegisterCard(typeof(LinneaCardPool))]
public sealed class LayeredSelection : LinneaCardBase
{
    protected override IEnumerable<IHoverTip> AdditionalHoverTips => new IHoverTip[] { HoverTipFactory.FromCard<Ore>() };

    public LayeredSelection() : base(1, CardType.Skill, CardRarity.Uncommon, TargetType.Self) { }
    protected override async Task OnPlay(PlayerChoiceContext context, CardPlay play)
    {
        await PowerCmd.Apply<LayeredSelectionPower>(context, Owner.Creature, 1, Owner.Creature, this);
    }
    protected override void OnUpgrade()
    {
        AddKeyword(CardKeyword.Retain);
    }
}
