namespace LinneaMod.Content.Cards;

/// <summary>刨根问底：按原案与升级表实现。</summary>
[RegisterCard(typeof(LinneaCardPool))]
public sealed class DigDeep : LinneaCardBase
{
    protected override IEnumerable<DynamicVar> CanonicalVars => new DynamicVar[] { new DamageVar(10m, ValueProp.Move), new DynamicVar("Encourage", 5m) };
    public override IEnumerable<CardKeyword> CanonicalKeywords
    {
        get
        {
            HashSet<CardKeyword> result = new() {  };
            LinneaKeywords.AddTo(result, LinneaKeywords.Encourage);
            return result;
        }
    }

    public DigDeep() : base(2, CardType.Attack, CardRarity.Uncommon, TargetType.AllEnemies) { }
    protected override async Task OnPlay(PlayerChoiceContext context, CardPlay play)
    {
        await DamageCmd.Attack(DynamicVars.Damage.BaseValue).FromCard(this, play).TargetingAllOpponents(CombatState!).WithHitFx("vfx/vfx_attack_slash").Execute(context);
        await LumiCompanion.Encourage(context, Owner, DynamicVars["Encourage"].BaseValue, this);
    }
    protected override void OnUpgrade()
    {
        DynamicVars["Damage"].UpgradeValueBy(3m);
        DynamicVars["Encourage"].UpgradeValueBy(1m);
    }
}
