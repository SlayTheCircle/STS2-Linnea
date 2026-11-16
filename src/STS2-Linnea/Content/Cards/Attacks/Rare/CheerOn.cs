namespace LinneaMod.Content.Cards;

/// <summary>加油鼓劲：按原案与升级表实现。</summary>
[RegisterCard(typeof(LinneaCardPool))]
public sealed class CheerOn : LinneaCardBase
{
    protected override IEnumerable<DynamicVar> CanonicalVars => new DynamicVar[] { new DamageVar(4m, ValueProp.Move), new DynamicVar("Foreknowledge", 2m), new DynamicVar("Encourage", 1m) };
    public override IEnumerable<CardKeyword> CanonicalKeywords
    {
        get
        {
            HashSet<CardKeyword> result = new() {  };
            LinneaKeywords.AddTo(result, LinneaKeywords.Encourage, LinneaKeywords.Foreknowledge);
            return result;
        }
    }

    private int _plays;
    private void ScaleEffects()
    {
        decimal factor = 1;
        for (int i = 0; i < _plays; i++) factor *= 2;
        DynamicVars.Damage.BaseValue = 4 * factor;
        DynamicVars["Foreknowledge"].BaseValue = 2 * factor;
        DynamicVars["Encourage"].BaseValue = factor;
    }
    protected override void AfterDowngraded() { base.AfterDowngraded(); ScaleEffects(); }

    public CheerOn() : base(0, CardType.Attack, CardRarity.Rare, TargetType.AnyEnemy) { }
    protected override async Task OnPlay(PlayerChoiceContext context, CardPlay play)
    {
        ArgumentNullException.ThrowIfNull(play.Target);

        decimal damage = DynamicVars.Damage.BaseValue;
        decimal foreknowledge = DynamicVars["Foreknowledge"].BaseValue;
        decimal encourage = DynamicVars["Encourage"].BaseValue;
        await DamageCmd.Attack(damage).FromCard(this, play).Targeting(play.Target!).Execute(context);
        await Foreknowledge.Gain(context, Owner, foreknowledge, this);
        await LumiCompanion.Encourage(context, Owner, encourage, this);
        EnergyCost.AddThisCombat(1);
        _plays++;
        ScaleEffects();
    }
    protected override void OnUpgrade()
    {
        AddKeyword(CardKeyword.Innate);
    }
}
