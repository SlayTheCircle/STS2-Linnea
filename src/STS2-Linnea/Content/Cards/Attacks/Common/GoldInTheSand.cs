namespace LinneaMod.Content.Cards;

/// <summary>沙里淘金：按原案与升级表实现。</summary>
[RegisterCard(typeof(LinneaCardPool))]
public sealed class GoldInTheSand : LinneaCardBase
{
    protected override IEnumerable<DynamicVar> CanonicalVars => new DynamicVar[] { new DamageVar(5m, ValueProp.Move) };
    public override IEnumerable<CardKeyword> CanonicalKeywords
    {
        get
        {
            HashSet<CardKeyword> result = new() { CardKeyword.Ethereal };
            return result;
        }
    }

    public GoldInTheSand() : base(1, CardType.Attack, CardRarity.Common, TargetType.AnyEnemy) { }
    protected override async Task OnPlay(PlayerChoiceContext context, CardPlay play)
    {
        ArgumentNullException.ThrowIfNull(play.Target);

        var attack = DamageCmd.Attack(DynamicVars.Damage.BaseValue).FromCard(this, play).Targeting(play.Target!);
        await attack.Execute(context);

    }
    protected override void OnUpgrade()
    {
        RemoveKeyword(CardKeyword.Ethereal);
    }
}
