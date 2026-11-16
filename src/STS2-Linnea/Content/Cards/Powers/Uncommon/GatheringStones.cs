namespace LinneaMod.Content.Cards;

/// <summary>聚沙成塔：按原案与升级表实现。</summary>
[RegisterCard(typeof(LinneaCardPool))]
public sealed class GatheringStones : LinneaCardBase
{
    protected override IEnumerable<DynamicVar> CanonicalVars => new DynamicVar[] { new DynamicVar("Block", 3m) };
    protected override IEnumerable<IHoverTip> AdditionalHoverTips => new[] { HoverTipFactory.FromPower<GatheringStonesPower>(DynamicVars["Block"].IntValue) };
    public GatheringStones() : base(2, CardType.Power, CardRarity.Uncommon, TargetType.Self) { }
    protected override async Task OnPlay(PlayerChoiceContext context, CardPlay play)
    {
        await PowerCmd.Apply<GatheringStonesPower>(context, Owner.Creature, DynamicVars["Block"].BaseValue, Owner.Creature, this);
    }
    protected override void OnUpgrade()
    {
        EnergyCost.UpgradeBy(-1);
    }
}
