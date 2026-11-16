namespace LinneaMod.Content.Cards;

/// <summary>精心挑选：按原案与升级表实现。</summary>
[RegisterCard(typeof(LinneaCardPool))]
public sealed class ChooseWithCare : LinneaCardBase
{
    protected override IEnumerable<IHoverTip> AdditionalHoverTips => new IHoverTip[] { HoverTipFactory.FromCard<Ore>() };
    protected override IEnumerable<DynamicVar> CanonicalVars => new DynamicVar[] { new DynamicVar("Ores", 3m), new BlockVar(6m, ValueProp.Move), new DynamicVar("ExtraBlock", 4m) };
    public override bool GainsBlock => true;
    public override CardMultiplayerConstraint MultiplayerConstraint => CardMultiplayerConstraint.MultiplayerOnly;
    public ChooseWithCare() : base(1, CardType.Skill, CardRarity.Uncommon, TargetType.AnyAlly) { }
    protected override async Task OnPlay(PlayerChoiceContext context, CardPlay play)
    {

        if (play.Target?.Player is not { } ally) return;
        var ores = LinneaCards.Hand(Owner).OfType<Ore>().Cast<CardModel>().ToArray();
        int exhausted = 0;
        if (ores.Length > 0)
        {
            var prefs = new CardSelectorPrefs(SelectionScreenPrompt, 0, Math.Min(DynamicVars["Ores"].IntValue, ores.Length)) { RequireManualConfirmation = true };
            foreach (var ore in await CardSelectCmd.FromSimpleGrid(context, ores, Owner, prefs))
                if (ore.Pile?.Type == PileType.Hand) { await CardCmd.Exhaust(context, ore); exhausted++; }
        }
        await CreatureCmd.GainBlock(ally.Creature, DynamicVars.Block, play);
        if (exhausted > 0) await CreatureCmd.GainBlock(ally.Creature, exhausted * DynamicVars["ExtraBlock"].BaseValue, ValueProp.Move, play);
    }
    protected override void OnUpgrade()
    {
        DynamicVars["Ores"].UpgradeValueBy(2m);
    }
}
