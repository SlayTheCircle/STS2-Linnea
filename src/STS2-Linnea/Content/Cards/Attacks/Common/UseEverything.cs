namespace LinneaMod.Content.Cards;

/// <summary>物尽其用：按原案与升级表实现。</summary>
[RegisterCard(typeof(LinneaCardPool))]
public sealed class UseEverything : LinneaCardBase
{
    protected override IEnumerable<IHoverTip> AdditionalHoverTips => new IHoverTip[] { HoverTipFactory.FromCard<Ore>() };
    protected override IEnumerable<DynamicVar> CanonicalVars => new DynamicVar[] { new DamageVar(7m, ValueProp.Move), new DynamicVar("ExtraDamage", 8m) };
    public override IEnumerable<CardKeyword> CanonicalKeywords
    {
        get
        {
            HashSet<CardKeyword> result = new() {  };
            return result;
        }
    }

    public UseEverything() : base(1, CardType.Attack, CardRarity.Common, TargetType.AnyEnemy) { }
    protected override async Task OnPlay(PlayerChoiceContext context, CardPlay play)
    {
        ArgumentNullException.ThrowIfNull(play.Target);
        await DamageCmd.Attack(DynamicVars.Damage.BaseValue).FromCard(this, play).Targeting(play.Target!).WithHitFx("vfx/vfx_attack_slash").Execute(context);
        if (!PileType.Hand.GetPile(Owner).Cards.OfType<Ore>().Any())
        {
            await CardPileCmd.AddGeneratedCardToCombat(CombatState!.CreateCard<Ore>(Owner), PileType.Hand, Owner);
            await DamageCmd.Attack(DynamicVars["ExtraDamage"].BaseValue).FromCard(this, play).Targeting(play.Target!).WithHitFx("vfx/vfx_attack_slash").Execute(context);
        }
    }
    protected override void OnUpgrade()
    {
        DynamicVars["Damage"].UpgradeValueBy(2m);
        DynamicVars["ExtraDamage"].UpgradeValueBy(2m);
    }
}
