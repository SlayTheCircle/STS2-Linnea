namespace LinneaMod.Content.Cards;

/// <summary>勤加练习：按原案与升级表实现。</summary>
[RegisterCard(typeof(LinneaCardPool))]
public sealed class DailyPractice : LinneaCardBase
{
    protected override IEnumerable<DynamicVar> CanonicalVars => new DynamicVar[] { new DynamicVar("Effect", 3m) };
    public override IEnumerable<CardKeyword> CanonicalKeywords
    {
        get
        {
            HashSet<CardKeyword> result = new() {  };
            LinneaKeywords.AddTo(result, LinneaKeywords.Encourage);
            return result;
        }
    }

    public DailyPractice() : base(1, CardType.Power, CardRarity.Rare, TargetType.Self) { }
    protected override async Task OnPlay(PlayerChoiceContext context, CardPlay play)
    {
        await PowerCmd.Apply<DailyPracticePower>(context, Owner.Creature, DynamicVars["Effect"].BaseValue, Owner.Creature, this);
    }
    protected override void OnUpgrade()
    {
        DynamicVars["Effect"].UpgradeValueBy(1m);
    }
}
