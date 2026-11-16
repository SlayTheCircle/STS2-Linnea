namespace LinneaMod.Content.Cards;

/// <summary>呼朋唤友：按原案与升级表实现。</summary>
[RegisterCard(typeof(LinneaCardPool))]
public sealed class CallAFriend : LinneaCardBase
{
    protected override IEnumerable<IHoverTip> AdditionalHoverTips => new IHoverTip[] { HoverTipFactory.FromCard<Lumi>() };
    protected override IEnumerable<DynamicVar> CanonicalVars => new DynamicVar[] { new BlockVar(5m, ValueProp.Move) };
    public override bool GainsBlock => true;

    public CallAFriend() : base(1, CardType.Skill, CardRarity.Common, TargetType.Self) { }
    protected override async Task OnPlay(PlayerChoiceContext context, CardPlay play)
    {
        await CreatureCmd.GainBlock(Owner.Creature, DynamicVars.Block, play);
        await CardCmd.Discard(context, await CardSelectCmd.FromHandForDiscard(context, Owner, new CardSelectorPrefs(CardSelectorPrefs.DiscardSelectionPrompt, 1), null, this));
        await LumiCompanion.Recall(context, Owner);
    }
    protected override void OnUpgrade()
    {
        DynamicVars["Block"].UpgradeValueBy(2m);
    }
}
