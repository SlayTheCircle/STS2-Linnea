namespace LinneaMod.Content.Cards;

/// <summary>金属暴风：按原案与升级表实现。</summary>
[RegisterCard(typeof(LinneaCardPool))]
public sealed class MetalStorm : LinneaCardBase
{
    protected override IEnumerable<DynamicVar> CanonicalVars => new DynamicVar[] { new DamageVar(3m, ValueProp.Move) };

    protected override bool HasEnergyCostX => true;
    public MetalStorm() : base(0, CardType.Attack, CardRarity.Rare, TargetType.AnyEnemy) { }
    protected override async Task OnPlay(PlayerChoiceContext context, CardPlay play)
    {
        ArgumentNullException.ThrowIfNull(play.Target);

        int energy = ResolveEnergyXValue();
        var statuses = LinneaCards.Hand(Owner).Where(c => c.Type == CardType.Status).ToArray();
        foreach (var status in statuses) if (status.Pile?.Type == PileType.Hand) await CardCmd.Exhaust(context, status);
        if (statuses.Length > 0)
        {
        await DamageCmd.Attack(DynamicVars.Damage.BaseValue * energy).FromCard(this, play).Targeting(play.Target!).WithHitCount(statuses.Length).WithHitFx("vfx/vfx_attack_slash").Execute(context);
        }
    }
    protected override void OnUpgrade()
    {
        DynamicVars["Damage"].UpgradeValueBy(1m);
    }
}
