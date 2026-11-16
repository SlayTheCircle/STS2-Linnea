namespace LinneaMod.Content.Relics;

[RegisterRelic(typeof(LinneaRelicPool))]
public sealed class FestivalCertificate : LinneaRelicBase
{
    public override RelicRarity Rarity => RelicRarity.Rare;

    private PlayerChoiceContext? _damageContext;
    protected override IEnumerable<IHoverTip> AdditionalHoverTips => new[] { HoverTipFactory.FromCard<Lumi>() };
    public override Task BeforeDamageReceived(PlayerChoiceContext context, Creature target, decimal amount, ValueProp props, Creature? dealer, CardModel? source)
    {
        if (target == Owner.Creature) _damageContext = context;
        return Task.CompletedTask;
    }
    public override decimal ModifyHpLostAfterOsty(Creature target, decimal amount, ValueProp props, Creature? dealer, CardModel? source)
        => target == Owner.Creature && props.IsPoweredAttack() && amount > 0 && LumiCompanion.Find(Owner) is { Pile.Type: PileType.Hand }
            ? Math.Max(0, amount - 6) : amount;
    public override async Task AfterModifyingHpLostAfterOsty()
    {
        if (LumiCompanion.Find(Owner) is not { Pile.Type: PileType.Hand } lumi) return;
        Flash();
        await CardCmd.Discard(_damageContext ?? throw new InvalidOperationException("Damage context missing"), lumi);
    }
}
