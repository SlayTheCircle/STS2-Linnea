namespace LinneaMod.Content.Cards;

/// <summary>攻守相济：按原案与升级表实现。</summary>
[RegisterCard(typeof(LinneaCardPool))]
public sealed class OffenseAndDefense : LinneaCardBase
{
    protected override IEnumerable<DynamicVar> CanonicalVars => new DynamicVar[] { new BlockVar(7m, ValueProp.Move), new DynamicVar("Encourage", 3m) };
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

    public OffenseAndDefense() : base(1, CardType.Attack, CardRarity.Common, TargetType.Self) { }
    protected override async Task OnPlay(PlayerChoiceContext context, CardPlay play)
    {
        await CreatureCmd.GainBlock(Owner.Creature, DynamicVars.Block, play);
        await LumiCompanion.Encourage(context, Owner, DynamicVars["Encourage"].BaseValue, this);
    }
    protected override void OnUpgrade()
    {
        DynamicVars["Block"].UpgradeValueBy(2m);
        DynamicVars["Encourage"].UpgradeValueBy(3m);
    }
}
