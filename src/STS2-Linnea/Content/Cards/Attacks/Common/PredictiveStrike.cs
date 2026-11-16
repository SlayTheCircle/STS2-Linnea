namespace LinneaMod.Content.Cards;

/// <summary>预判打击：按原案与升级表实现。</summary>
[RegisterCard(typeof(LinneaCardPool))]
public sealed class PredictiveStrike : LinneaCardBase
{
    protected override IEnumerable<DynamicVar> CanonicalVars => new DynamicVar[] { new DamageVar(9m, ValueProp.Move), new DynamicVar("Foreknowledge", 5m) };
    public override IEnumerable<CardKeyword> CanonicalKeywords
    {
        get
        {
            HashSet<CardKeyword> result = new() {  };
            LinneaKeywords.AddTo(result, LinneaKeywords.Foreknowledge);
            return result;
        }
    }
    protected override HashSet<CardTag> CanonicalTags => new() { CardTag.Strike };

    public PredictiveStrike() : base(1, CardType.Attack, CardRarity.Common, TargetType.AnyEnemy) { }
    protected override async Task OnPlay(PlayerChoiceContext context, CardPlay play)
    {
        ArgumentNullException.ThrowIfNull(play.Target);
        await DamageCmd.Attack(DynamicVars.Damage.BaseValue).FromCard(this, play).Targeting(play.Target!).WithHitFx("vfx/vfx_attack_slash").Execute(context);
        await Foreknowledge.Gain(context, Owner, DynamicVars["Foreknowledge"].BaseValue, this);
    }
    protected override void OnUpgrade()
    {
        DynamicVars["Damage"].UpgradeValueBy(1m);
        DynamicVars["Foreknowledge"].UpgradeValueBy(2m);
    }
}
