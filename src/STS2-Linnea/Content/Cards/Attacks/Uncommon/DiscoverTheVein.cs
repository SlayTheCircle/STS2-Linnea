namespace LinneaMod.Content.Cards;

/// <summary>发现矿脉：按原案与升级表实现。</summary>
[RegisterCard(typeof(LinneaCardPool))]
public sealed class DiscoverTheVein : LinneaCardBase
{
    protected override IEnumerable<IHoverTip> AdditionalHoverTips => new IHoverTip[] { HoverTipFactory.FromCard<Ore>() };
    protected override IEnumerable<DynamicVar> CanonicalVars => new DynamicVar[] { new DamageVar(33m, ValueProp.Move), new DynamicVar("Ore", 3m) };
    public override IEnumerable<CardKeyword> CanonicalKeywords
    {
        get
        {
            HashSet<CardKeyword> result = new() {  };
            return result;
        }
    }

    public DiscoverTheVein() : base(3, CardType.Attack, CardRarity.Uncommon, TargetType.AnyEnemy) { }
    protected override async Task OnPlay(PlayerChoiceContext context, CardPlay play)
    {
        ArgumentNullException.ThrowIfNull(play.Target);
        await DamageCmd.Attack(DynamicVars.Damage.BaseValue).FromCard(this, play).Targeting(play.Target!).WithHitFx("vfx/vfx_attack_slash").Execute(context);
        await CardPileCmd.AddToCombatAndPreview<Ore>(Owner.Creature, PileType.Hand, DynamicVars["Ore"].IntValue, Owner);
    }
    protected override void OnUpgrade()
    {
        DynamicVars["Damage"].UpgradeValueBy(11m);
        DynamicVars["Ore"].UpgradeValueBy(1m);
    }
}
