namespace LinneaMod.Content.Cards;

/// <summary>蓄势待发：按原案与升级表实现。</summary>
[RegisterCard(typeof(LinneaCardPool))]
public sealed class ReadyToGo : LinneaCardBase
{
    protected override IEnumerable<DynamicVar> CanonicalVars => new DynamicVar[] { new BlockVar(10m, ValueProp.Move), new DynamicVar("Encourage", 6m) };
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

    public ReadyToGo() : base(2, CardType.Skill, CardRarity.Uncommon, TargetType.Self) { }
    protected override async Task OnPlay(PlayerChoiceContext context, CardPlay play)
    {
        await CreatureCmd.GainBlock(Owner.Creature, DynamicVars.Block, play);
        await PowerCmd.Apply<EncourageNextTurnPower>(context, Owner.Creature, DynamicVars["Encourage"].BaseValue, Owner.Creature, this);
    }
    protected override void OnUpgrade()
    {
        DynamicVars["Block"].UpgradeValueBy(3m);
        DynamicVars["Encourage"].UpgradeValueBy(2m);
    }
}
