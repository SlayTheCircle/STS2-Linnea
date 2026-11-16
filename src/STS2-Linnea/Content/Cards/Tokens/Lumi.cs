namespace LinneaMod.Content.Cards;

[RegisterCard(typeof(LinneaCardPool))]
public sealed class Lumi : LinneaCardBase
{
    public decimal CumulativeDamage { get; private set; }
    public decimal DamageDealt { get; private set; }
    public bool DealtUnblockedDamage { get; private set; }
    public override Task BeforeCardPlayed(CardPlay play)
    {
        if (play.Card == this) { DamageDealt = 0; DealtUnblockedDamage = false; }
        return Task.CompletedTask;
    }
    public bool EnabledThisTurn { get; private set; }
    protected override bool IsPlayable => EnabledThisTurn;
    public override CardPoolModel VisualCardPool => ModelDb.CardPool<MegaCrit.Sts2.Core.Models.CardPools.ColorlessCardPool>();
    public override IEnumerable<CardKeyword> CanonicalKeywords => new[] { CardKeyword.Retain };
    protected override IEnumerable<DynamicVar> CanonicalVars => new DynamicVar[] { new DamageVar(5m, ValueProp.Move), new CardsVar(1) };
    public Lumi() : base(0, CardType.Attack, CardRarity.Token, TargetType.AnyEnemy) { }

    public void EnableForTurn() { AssertMutable(); EnabledThisTurn = true; }

    protected override async Task OnPlay(PlayerChoiceContext context, CardPlay play)
    {
        ArgumentNullException.ThrowIfNull(play.Target);
        var targets = Owner.Creature.HasPower<AllInPower>()
            ? CombatState!.HittableEnemies.ToArray() : new[] { play.Target };
        var attack = DamageCmd.Attack(DynamicVars.Damage.BaseValue).FromCard(this, play)
            .WithHitCount(Owner.Creature.HasPower<ContinuousHammerPower>() ? 2 : 1);
        if (Owner.Creature.GetPower<WeaknessSurveyPower>() is { } survey)
            attack.BeforeDamage(async () =>
            {
                foreach (var target in targets.Where(t => t.IsHittable))
                    await PowerCmd.Apply<VulnerablePower>(context, target, survey.Amount, Owner.Creature, this);
            });
        if (Owner.Creature.HasPower<AllInPower>()) attack.TargetingAllOpponents(CombatState!);
        else attack.Targeting(play.Target);
        await attack.WithHitFx("vfx/vfx_attack_blunt").Execute(context);
        CumulativeDamage += attack.Results.SelectMany(r => r).Sum(r => r.TotalDamage);
        DamageDealt += attack.Results.SelectMany(r => r).Sum(r => r.TotalDamage);
        DealtUnblockedDamage |= attack.Results.SelectMany(r => r).Any(r => r.UnblockedDamage > 0);
        await CardPileCmd.Draw(context, DynamicVars.Cards.BaseValue, Owner);
    }

    public override Task AfterSideTurnEnd(PlayerChoiceContext context, CombatSide side, IEnumerable<Creature> participants)
    {
        if (participants.Contains(Owner.Creature)) EnabledThisTurn = false;
        return Task.CompletedTask;
    }

    protected override void OnUpgrade()
    {
        DynamicVars.Damage.UpgradeValueBy(3m);
        DynamicVars.Cards.UpgradeValueBy(1m);
    }
}
