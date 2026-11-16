namespace LinneaMod.Content.Cards;

/// <summary>日积月累：按原案与升级表实现。</summary>
[RegisterCard(typeof(LinneaCardPool))]
public sealed class DailyAccumulation : LinneaCardBase
{
    public override IEnumerable<CardKeyword> CanonicalKeywords
    {
        get
        {
            HashSet<CardKeyword> result = new() {  };
            LinneaKeywords.AddTo(result, LinneaKeywords.Encourage, LinneaKeywords.Foreknowledge);
            return result;
        }
    }
    protected override IEnumerable<IHoverTip> AdditionalHoverTips => new[] { HoverTipFactory.FromPower<DailyAccumulationPower>(1) };
    public DailyAccumulation() : base(1, CardType.Power, CardRarity.Uncommon, TargetType.Self) { }
    protected override async Task OnPlay(PlayerChoiceContext context, CardPlay play)
    {
        await PowerCmd.Apply<DailyAccumulationPower>(context, Owner.Creature, 1, Owner.Creature, this);
    }
    protected override void OnUpgrade()
    {
        EnergyCost.UpgradeBy(-1);
    }
}
