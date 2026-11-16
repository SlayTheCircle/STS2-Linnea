namespace LinneaMod.Content.Cards;

/// <summary>敏锐洞察：按原案与升级表实现。</summary>
[RegisterCard(typeof(LinneaCardPool))]
public sealed class SharpInsight : LinneaCardBase
{
    protected override IEnumerable<DynamicVar> CanonicalVars => new DynamicVar[] { new DamageVar(9m, ValueProp.Move), new DynamicVar("Foreknowledge", 6m) };
    public override IEnumerable<CardKeyword> CanonicalKeywords
    {
        get
        {
            HashSet<CardKeyword> result = new() {  };
            LinneaKeywords.AddTo(result, LinneaKeywords.Foreknowledge);
            return result;
        }
    }

    public SharpInsight() : base(1, CardType.Attack, CardRarity.Rare, TargetType.AnyEnemy) { }
    protected override async Task OnPlay(PlayerChoiceContext context, CardPlay play)
    {
        ArgumentNullException.ThrowIfNull(play.Target);
        await DamageCmd.Attack(DynamicVars.Damage.BaseValue).FromCard(this, play).Targeting(play.Target!).WithHitFx("vfx/vfx_attack_slash").Execute(context);
        await Foreknowledge.Gain(context, Owner, DynamicVars["Foreknowledge"].BaseValue, this);
        if (Owner.Creature.GetPower<LinneaBattleRecordPower>()?.UnharmedLastTurn == true) BaseReplayCount++;
    }
    protected override void OnUpgrade()
    {
        DynamicVars["Damage"].UpgradeValueBy(2m);
        DynamicVars["Foreknowledge"].UpgradeValueBy(2m);
    }
}
