namespace LinneaMod.Content.Cards;

/// <summary>沙锤打击：按原案与升级表实现。</summary>
[RegisterCard(typeof(LinneaCardPool))]
public sealed class HammerStrike : LinneaCardBase
{
    protected override IEnumerable<DynamicVar> CanonicalVars => new DynamicVar[] { new DamageVar(7m, ValueProp.Move), new DynamicVar("Encourage", 5m) };
    public override IEnumerable<CardKeyword> CanonicalKeywords
    {
        get
        {
            HashSet<CardKeyword> result = new() {  };
            LinneaKeywords.AddTo(result, LinneaKeywords.Encourage);
            return result;
        }
    }
    protected override HashSet<CardTag> CanonicalTags => new() { CardTag.Strike };

    public HammerStrike() : base(1, CardType.Attack, CardRarity.Common, TargetType.AnyEnemy) { }
    protected override async Task OnPlay(PlayerChoiceContext context, CardPlay play)
    {
        ArgumentNullException.ThrowIfNull(play.Target);
        await DamageCmd.Attack(DynamicVars.Damage.BaseValue).FromCard(this, play).Targeting(play.Target!).WithHitFx("vfx/vfx_attack_slash").Execute(context);
        await LumiCompanion.Encourage(context, Owner, DynamicVars["Encourage"].BaseValue, this);
    }
    protected override void OnUpgrade()
    {
        DynamicVars["Damage"].UpgradeValueBy(3m);
        DynamicVars["Encourage"].UpgradeValueBy(2m);
    }
}
