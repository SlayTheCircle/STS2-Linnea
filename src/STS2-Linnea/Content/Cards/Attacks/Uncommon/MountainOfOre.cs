namespace LinneaMod.Content.Cards;

/// <summary>堆积如山：按原案与升级表实现。</summary>
[RegisterCard(typeof(LinneaCardPool))]
public sealed class MountainOfOre : LinneaCardBase
{
    protected override IEnumerable<IHoverTip> AdditionalHoverTips => new IHoverTip[] { HoverTipFactory.FromCard<Ore>() };
    protected override IEnumerable<DynamicVar> CanonicalVars => new DynamicVar[] { new DamageVar(10m, ValueProp.Move) };

    public MountainOfOre() : base(1, CardType.Attack, CardRarity.Uncommon, TargetType.AnyEnemy) { }
    protected override async Task OnPlay(PlayerChoiceContext context, CardPlay play)
    {
        ArgumentNullException.ThrowIfNull(play.Target);
        await DamageCmd.Attack(DynamicVars.Damage.BaseValue).FromCard(this, play).Targeting(play.Target!).WithHitFx("vfx/vfx_attack_slash").Execute(context);
        await LinneaCards.Ore(Owner, PileType.Hand, 1);
        await LinneaCards.Ore(Owner, PileType.Draw, LinneaCards.Hand(Owner).OfType<Ore>().Count());
    }
    protected override void OnUpgrade()
    {
        DynamicVars["Damage"].UpgradeValueBy(5m);
    }
}
