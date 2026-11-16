namespace LinneaMod.Content.Cards;

/// <summary>坚实基础：按原案与升级表实现。</summary>
[RegisterCard(typeof(LinneaCardPool))]
public sealed class SolidFoundation : LinneaCardBase
{
    protected override IEnumerable<DynamicVar> CanonicalVars => new DynamicVar[] { new DynamicVar("Effect", 2m) };
    public override IEnumerable<CardKeyword> CanonicalKeywords
    {
        get
        {
            HashSet<CardKeyword> result = new() {  };
            LinneaKeywords.AddTo(result, LinneaKeywords.Encourage);
            return result;
        }
    }

    public SolidFoundation() : base(2, CardType.Power, CardRarity.Rare, TargetType.Self) { }
    protected override async Task OnPlay(PlayerChoiceContext context, CardPlay play)
    {
        await PowerCmd.Apply<SolidFoundationPower>(context, Owner.Creature, DynamicVars["Effect"].BaseValue, Owner.Creature, this);
    }
    protected override void OnUpgrade()
    {
        DynamicVars["Effect"].UpgradeValueBy(2m);
    }
}
