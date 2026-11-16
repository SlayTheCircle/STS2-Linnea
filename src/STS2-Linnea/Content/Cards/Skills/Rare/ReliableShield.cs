namespace LinneaMod.Content.Cards;

/// <summary>可靠后盾：按原案与升级表实现。</summary>
[RegisterCard(typeof(LinneaCardPool))]
public sealed class ReliableShield : LinneaCardBase
{
    protected override IEnumerable<IHoverTip> AdditionalHoverTips => new IHoverTip[] { HoverTipFactory.FromCard<Lumi>() };
    public override bool GainsBlock => true;
    public ReliableShield() : base(1, CardType.Skill, CardRarity.Rare, TargetType.AnyEnemy) { }
    protected override async Task OnPlay(PlayerChoiceContext context, CardPlay play)
    {
        ArgumentNullException.ThrowIfNull(play.Target);

        if (IsUpgraded) await LumiCompanion.Recall(context, Owner);
        decimal damage = await LumiCompanion.Play(context, Owner, play.Target);
        await CreatureCmd.GainBlock(Owner.Creature, damage, ValueProp.Unpowered, play);
    }
    protected override void OnUpgrade()
    {

    }
}
