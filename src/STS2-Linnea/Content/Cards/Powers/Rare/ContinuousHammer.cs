namespace LinneaMod.Content.Cards;

/// <summary>连环锤击：按原案与升级表实现。</summary>
[RegisterCard(typeof(LinneaCardPool))]
public sealed class ContinuousHammer : LinneaCardBase
{
    protected override IEnumerable<IHoverTip> AdditionalHoverTips => new[] { HoverTipFactory.FromPower<ContinuousHammerPower>(1) };
    public ContinuousHammer() : base(2, CardType.Power, CardRarity.Rare, TargetType.Self) { }
    protected override async Task OnPlay(PlayerChoiceContext context, CardPlay play)
    {
        await PowerCmd.Apply<ContinuousHammerPower>(context, Owner.Creature, 1, Owner.Creature, this);
        await LumiCompanion.Recall(context, Owner);
    }
    protected override void OnUpgrade()
    {
        EnergyCost.UpgradeBy(-1);
    }
}
