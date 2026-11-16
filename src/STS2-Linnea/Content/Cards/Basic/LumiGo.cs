namespace LinneaMod.Content.Cards;

/// <summary>露米出击！：按原案与升级表实现。</summary>
[RegisterCard(typeof(LinneaCardPool))]
[RegisterArchaicToothTranscendence(typeof(LumiShout))]
public sealed class LumiGo : LinneaCardBase
{
    protected override IEnumerable<DynamicVar> CanonicalVars => new DynamicVar[] { new BlockVar(3m, ValueProp.Move) };
    public override bool GainsBlock => true;
    protected override IEnumerable<IHoverTip> AdditionalHoverTips => new[] { HoverTipFactory.FromCard<Lumi>() };
    public LumiGo() : base(1, CardType.Skill, CardRarity.Basic, TargetType.Self) { }
    protected override async Task OnPlay(PlayerChoiceContext context, CardPlay play)
    {
        await CreatureCmd.GainBlock(Owner.Creature, DynamicVars.Block, play);
        if (LumiCompanion.Find(Owner) is { Pile.Type: PileType.Hand } lumi) lumi.EnableForTurn();
    }
    protected override void OnUpgrade()
    {
        DynamicVars["Block"].UpgradeValueBy(2m);
    }
}
