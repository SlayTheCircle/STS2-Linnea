namespace LinneaMod.Content.Cards;

/// <summary>眺望未来：按原案与升级表实现。</summary>
[RegisterCard(typeof(LinneaCardPool))]
public sealed class GazeAhead : LinneaCardBase
{
    protected override IEnumerable<DynamicVar> CanonicalVars => new DynamicVar[] { new DamageVar(4m, ValueProp.Move), new DynamicVar("Spend", 1m), new DynamicVar("Increase", 2m) };
    public override IEnumerable<CardKeyword> CanonicalKeywords
    {
        get
        {
            HashSet<CardKeyword> result = new() {  };
            LinneaKeywords.AddTo(result, LinneaKeywords.Foreknowledge);
            return result;
        }
    }

    public GazeAhead() : base(0, CardType.Attack, CardRarity.Rare, TargetType.AnyEnemy) { }
    protected override async Task OnPlay(PlayerChoiceContext context, CardPlay play)
    {
        ArgumentNullException.ThrowIfNull(play.Target);

        int spent = await Foreknowledge.Spend(context, Owner, Foreknowledge.Amount(Owner));
        decimal amount = DynamicVars.Damage.BaseValue + (spent / DynamicVars["Spend"].IntValue) * DynamicVars["Increase"].BaseValue;
        await DamageCmd.Attack(amount).FromCard(this, play).Targeting(play.Target!).WithHitFx("vfx/vfx_attack_slash").Execute(context);
    }
    protected override void OnUpgrade()
    {
        DynamicVars["Damage"].UpgradeValueBy(2m);
        DynamicVars["Increase"].UpgradeValueBy(1m);
    }
}
