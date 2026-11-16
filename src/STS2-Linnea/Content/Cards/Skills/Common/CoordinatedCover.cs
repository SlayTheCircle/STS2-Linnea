namespace LinneaMod.Content.Cards;

/// <summary>协同掩护：按原案与升级表实现。</summary>
[RegisterCard(typeof(LinneaCardPool))]
public sealed class CoordinatedCover : LinneaCardBase
{
    protected override IEnumerable<IHoverTip> AdditionalHoverTips => new IHoverTip[] { HoverTipFactory.FromCard<Lumi>() };
    protected override IEnumerable<DynamicVar> CanonicalVars => new DynamicVar[] { new BlockVar(3m, ValueProp.Move), new DynamicVar("ExtraBlock", 8m) };
    public override bool GainsBlock => true;

    public CoordinatedCover() : base(0, CardType.Skill, CardRarity.Common, TargetType.Self) { }
    protected override async Task OnPlay(PlayerChoiceContext context, CardPlay play)
    {
        await CreatureCmd.GainBlock(Owner.Creature, DynamicVars.Block, play);
        var discarded = (await CardSelectCmd.FromHandForDiscard(context, Owner, new CardSelectorPrefs(CardSelectorPrefs.DiscardSelectionPrompt, 1), null, this)).ToArray();
        await CardCmd.Discard(context, discarded);
        if (discarded.Any(c => c is Lumi)) await CreatureCmd.GainBlock(Owner.Creature, DynamicVars["ExtraBlock"].BaseValue, ValueProp.Move, play);
    }
    protected override void OnUpgrade()
    {
        DynamicVars["Block"].UpgradeValueBy(2m);
        DynamicVars["ExtraBlock"].UpgradeValueBy(2m);
    }
}
