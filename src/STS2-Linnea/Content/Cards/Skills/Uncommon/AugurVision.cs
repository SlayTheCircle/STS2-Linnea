namespace LinneaMod.Content.Cards;

/// <summary>谶鸟视野：按原案与升级表实现。</summary>
[RegisterCard(typeof(LinneaCardPool))]
public sealed class AugurVision : LinneaCardBase
{
    protected override IEnumerable<DynamicVar> CanonicalVars => new DynamicVar[] { new CardsVar(1), new DynamicVar("Foreknowledge", 9m) };
    public override IEnumerable<CardKeyword> CanonicalKeywords
    {
        get
        {
            HashSet<CardKeyword> result = new() {  };
            LinneaKeywords.AddTo(result, LinneaKeywords.Foreknowledge);
            return result;
        }
    }

    public AugurVision() : base(1, CardType.Skill, CardRarity.Uncommon, TargetType.Self) { }
    protected override async Task OnPlay(PlayerChoiceContext context, CardPlay play)
    {
        await CardPileCmd.Draw(context, DynamicVars.Cards.BaseValue, Owner);
        await Foreknowledge.Gain(context, Owner, DynamicVars["Foreknowledge"].BaseValue, this);
        await PowerCmd.Apply<VisionNextTurnPower>(context, Owner.Creature, DynamicVars["Foreknowledge"].BaseValue, Owner.Creature, this);
    }
    protected override void OnUpgrade()
    {
        DynamicVars["Foreknowledge"].UpgradeValueBy(2m);
    }
}
