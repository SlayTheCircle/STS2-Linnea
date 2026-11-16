namespace LinneaMod.Content.Cards;

/// <summary>畅想时刻：按原案与升级表实现。</summary>
[RegisterCard(typeof(LinneaCardPool))]
public sealed class Reverie : LinneaCardBase
{
    protected override IEnumerable<DynamicVar> CanonicalVars => new DynamicVar[] { new DynamicVar("Encourage", 2m), new DynamicVar("Threshold", 5m) };
    public override IEnumerable<CardKeyword> CanonicalKeywords
    {
        get
        {
            HashSet<CardKeyword> result = new() {  };
            LinneaKeywords.AddTo(result, LinneaKeywords.Encourage, LinneaKeywords.Foreknowledge);
            return result;
        }
    }

    public Reverie() : base(0, CardType.Skill, CardRarity.Common, TargetType.Self) { }
    protected override async Task OnPlay(PlayerChoiceContext context, CardPlay play)
    {

        int spent = await Foreknowledge.Spend(context, Owner, 3);
        if (spent == 3) await LumiCompanion.Encourage(context, Owner, DynamicVars["Encourage"].BaseValue, this);
        if (Foreknowledge.Amount(Owner) >= DynamicVars["Threshold"].IntValue)
        {
            await Foreknowledge.Spend(context, Owner, 2);
            await CardPileCmd.Add(this, PileType.Hand);
        }
    }
    protected override void OnUpgrade()
    {
        AddKeyword(CardKeyword.Retain);
        DynamicVars["Encourage"].UpgradeValueBy(1m);
        DynamicVars["Threshold"].UpgradeValueBy(-1m);
    }
}
