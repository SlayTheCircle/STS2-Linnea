namespace LinneaMod.Content.Cards;

/// <summary>透支力量：按原案与升级表实现。</summary>
[RegisterCard(typeof(LinneaCardPool))]
public sealed class Overdraft : LinneaCardBase
{
    protected override IEnumerable<DynamicVar> CanonicalVars => new DynamicVar[] { new CardsVar(3), new DynamicVar("Foreknowledge", 12m), new DynamicVar("Discard", 3m) };
    public override IEnumerable<CardKeyword> CanonicalKeywords
    {
        get
        {
            HashSet<CardKeyword> result = new() {  };
            LinneaKeywords.AddTo(result, LinneaKeywords.Foreknowledge);
            return result;
        }
    }

    public Overdraft() : base(1, CardType.Skill, CardRarity.Uncommon, TargetType.Self) { }
    protected override async Task OnPlay(PlayerChoiceContext context, CardPlay play)
    {
        await CardPileCmd.Draw(context, DynamicVars.Cards.BaseValue, Owner);
        await Foreknowledge.Gain(context, Owner, DynamicVars["Foreknowledge"].BaseValue, this);
        await PowerCmd.Apply<OverdraftPower>(context, Owner.Creature, DynamicVars["Discard"].BaseValue, Owner.Creature, this);
    }
    protected override void OnUpgrade()
    {
        DynamicVars["Foreknowledge"].UpgradeValueBy(3m);
        DynamicVars["Discard"].UpgradeValueBy(-1m);
    }
}
