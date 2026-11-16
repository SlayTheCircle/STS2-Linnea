namespace LinneaMod.Content.Cards;

/// <summary>骤雨连击：按原案与升级表实现。</summary>
[RegisterCard(typeof(LinneaCardPool))]
public sealed class RainOfBlows : LinneaCardBase
{
    protected override IEnumerable<IHoverTip> AdditionalHoverTips => new IHoverTip[] { HoverTipFactory.FromPower<StrengthPower>() };
    protected override IEnumerable<DynamicVar> CanonicalVars => new DynamicVar[] { new DamageVar(3m, ValueProp.Move), new DynamicVar("Hits", 3m) };

    public RainOfBlows() : base(1, CardType.Attack, CardRarity.Common, TargetType.AnyEnemy) { }
    protected override async Task OnPlay(PlayerChoiceContext context, CardPlay play)
    {
        ArgumentNullException.ThrowIfNull(play.Target);

        var top = PileType.Draw.GetPile(Owner).Cards.FirstOrDefault();
        if (top is not null) await CardCmd.Exhaust(context, top);
        await DamageCmd.Attack(DynamicVars.Damage.BaseValue).FromCard(this, play).Targeting(play.Target!).WithHitCount(DynamicVars["Hits"].IntValue).WithHitFx("vfx/vfx_attack_slash").Execute(context);
        if (top?.Type == CardType.Skill) await PowerCmd.Apply<StrengthPower>(context, Owner.Creature, 1, Owner.Creature, this);
    }
    protected override void OnUpgrade()
    {
        DynamicVars["Hits"].UpgradeValueBy(1m);
    }
}
