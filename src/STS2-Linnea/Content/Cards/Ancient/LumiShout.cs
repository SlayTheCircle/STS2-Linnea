namespace LinneaMod.Content.Cards;

/// <summary>露米呀吼吼！：按原案与升级表实现。</summary>
[RegisterCard(typeof(LinneaCardPool))]
public sealed class LumiShout : LinneaCardBase
{
    protected override IEnumerable<IHoverTip> AdditionalHoverTips => new IHoverTip[] { HoverTipFactory.FromCard<Lumi>() };
    protected override IEnumerable<DynamicVar> CanonicalVars => new DynamicVar[] { new BlockVar(9m, ValueProp.Move), new DynamicVar("Encourage", 10m) };
    public override bool GainsBlock => true;
    public override IEnumerable<CardKeyword> CanonicalKeywords
    {
        get
        {
            HashSet<CardKeyword> result = new() {  };
            LinneaKeywords.AddTo(result, LinneaKeywords.Encourage);
            return result;
        }
    }

    public LumiShout() : base(1, CardType.Skill, CardRarity.Ancient, TargetType.Self) { }
    protected override async Task OnPlay(PlayerChoiceContext context, CardPlay play)
    {
        await CreatureCmd.GainBlock(Owner.Creature, DynamicVars.Block, play);
        await LumiCompanion.Encourage(context, Owner, DynamicVars["Encourage"].BaseValue, this);
        var lumi = await LumiCompanion.Recall(context, Owner);
        lumi?.EnableForTurn();
    }
    protected override void OnUpgrade()
    {
        DynamicVars["Block"].UpgradeValueBy(4m);
        DynamicVars["Encourage"].UpgradeValueBy(5m);
    }
}
