namespace LinneaMod.Content.Cards;

/// <summary>舍身突击：按原案与升级表实现。</summary>
[RegisterCard(typeof(LinneaCardPool))]
public sealed class SacrificialAssault : LinneaCardBase
{
    protected override IEnumerable<IHoverTip> AdditionalHoverTips => new IHoverTip[] { HoverTipFactory.FromCard<Lumi>() };

    public SacrificialAssault() : base(0, CardType.Attack, CardRarity.Uncommon, TargetType.AnyEnemy) { }
    protected override async Task OnPlay(PlayerChoiceContext context, CardPlay play)
    {
        ArgumentNullException.ThrowIfNull(play.Target);

        if (LumiCompanion.Find(Owner) is { Pile.Type: PileType.Hand } lumi)
        {
            await CardCmd.AutoPlay(context, lumi, play.Target);
            await CardCmd.AutoPlay(context, lumi, play.Target);
        }
        if (Owner.Creature.GetPower<EncouragementPower>() is { } encouragement) await PowerCmd.Remove(encouragement);
        await PowerCmd.Apply<RecallLumiNextTurnPower>(context, Owner.Creature, 1, Owner.Creature, this);
    }
    public override async Task AfterCardPlayed(PlayerChoiceContext context, CardPlay play)
    {
        if (play.Card == this && LumiCompanion.Find(Owner) is { } lumi)
            await CardCmd.Exhaust(context, lumi);
    }
    protected override void OnUpgrade()
    {
        AddKeyword(CardKeyword.Retain);
    }
}
