namespace LinneaMod.Content.Cards;

/// <summary>示范打击：按原案与升级表实现。</summary>
[RegisterCard(typeof(LinneaCardPool))]
public sealed class Demonstration : LinneaCardBase
{
    protected override IEnumerable<IHoverTip> AdditionalHoverTips => new IHoverTip[] { HoverTipFactory.FromCard<Lumi>() };
    protected override IEnumerable<DynamicVar> CanonicalVars => new DynamicVar[] { new DynamicVar("Encourage", 5m) };
    public override IEnumerable<CardKeyword> CanonicalKeywords
    {
        get
        {
            HashSet<CardKeyword> result = new() {  };
            LinneaKeywords.AddTo(result, LinneaKeywords.Encourage);
            return result;
        }
    }

    public Demonstration() : base(1, CardType.Attack, CardRarity.Uncommon, TargetType.AnyEnemy) { }
    protected override async Task OnPlay(PlayerChoiceContext context, CardPlay play)
    {
        ArgumentNullException.ThrowIfNull(play.Target);

        decimal amount = LumiCompanion.Find(Owner)?.DynamicVars.Damage.BaseValue ?? (Owner.Relics.Any(r => r is SuperLumi) ? 8 : 5);
        amount += Owner.Creature.GetPower<EncouragementPower>()?.Amount ?? 0;
        await DamageCmd.Attack(amount).FromCard(this, play).Targeting(play.Target!).Execute(context);
        await PowerCmd.Apply<DemonstrationNextTurnPower>(context, Owner.Creature, DynamicVars["Encourage"].BaseValue, Owner.Creature, this);
    }
    protected override void OnUpgrade()
    {
        AddKeyword(CardKeyword.Retain);
    }
}
