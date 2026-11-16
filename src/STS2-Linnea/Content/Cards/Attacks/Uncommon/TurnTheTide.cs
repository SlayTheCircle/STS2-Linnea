namespace LinneaMod.Content.Cards;

/// <summary>扭转乾坤：按原案与升级表实现。</summary>
[RegisterCard(typeof(LinneaCardPool))]
public sealed class TurnTheTide : LinneaCardBase
{
    protected override IEnumerable<DynamicVar> CanonicalVars => new DynamicVar[] { new DamageVar(15m, ValueProp.Move), new DynamicVar("Threshold", 3m) };
    public override IEnumerable<CardKeyword> CanonicalKeywords
    {
        get
        {
            HashSet<CardKeyword> result = new() {  };
            LinneaKeywords.AddTo(result, LinneaKeywords.Foreknowledge);
            return result;
        }
    }

    public override bool TryModifyEnergyCostInCombat(CardModel card, decimal originalCost, out decimal modifiedCost)
    {
        int groups = card == this ? Foreknowledge.Amount(Owner) / DynamicVars["Threshold"].IntValue : 0;
        modifiedCost = groups > 0 ? Math.Max(0, originalCost - groups) : originalCost;
        return groups > 0;
    }
    public TurnTheTide() : base(2, CardType.Attack, CardRarity.Uncommon, TargetType.AnyEnemy) { }
    protected override async Task OnPlay(PlayerChoiceContext context, CardPlay play)
    {
        ArgumentNullException.ThrowIfNull(play.Target);
        await DamageCmd.Attack(DynamicVars.Damage.BaseValue).FromCard(this, play).Targeting(play.Target!).WithHitFx("vfx/vfx_attack_slash").Execute(context);
    }
    protected override void OnUpgrade()
    {
        DynamicVars["Damage"].UpgradeValueBy(4m);
        DynamicVars["Threshold"].UpgradeValueBy(-1m);
    }
}
