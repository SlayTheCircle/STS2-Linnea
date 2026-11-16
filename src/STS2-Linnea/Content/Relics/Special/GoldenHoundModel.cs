namespace LinneaMod.Content.Relics;

[RegisterRelic(typeof(LinneaRelicPool))]
public sealed class GoldenHoundModel : LinneaRelicBase
{
    public override RelicRarity Rarity => RelicRarity.Uncommon;

    private int _triggeredTurn = -1;
    public override Task BeforeCombatStart() { _triggeredTurn = -1; return Task.CompletedTask; }
    protected override IEnumerable<string> RegisteredKeywordIds => new[] { LinneaKeywords.Foreknowledge };
    public override async Task AfterDamageGiven(PlayerChoiceContext context, Creature? dealer, DamageResult result, ValueProp props, Creature target, CardModel? source)
    {
        if (dealer != Owner.Creature || result.TotalDamage <= 0) return;
        int turn = Owner.PlayerCombatState!.TurnNumber;
        if (_triggeredTurn == turn) return;
        _triggeredTurn = turn;
        Flash();
        await Foreknowledge.Gain(context, Owner, 4, source);
    }
}
