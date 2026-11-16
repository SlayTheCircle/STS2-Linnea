namespace LinneaMod.Content.Cards;

/// <summary>一语成谶：按原案与升级表实现。</summary>
[RegisterCard(typeof(LinneaCardPool))]
public sealed class SpokenOmen : LinneaCardBase
{
    protected override IEnumerable<DynamicVar> CanonicalVars => new DynamicVar[] { new DynamicVar("Damage", 4m), new DynamicVar("Block", 6m) };
    protected override IEnumerable<IHoverTip> AdditionalHoverTips => new[] { HoverTipFactory.FromPower<SpokenOmenPower>(DynamicVars["Damage"].IntValue) };
    public SpokenOmen() : base(2, CardType.Power, CardRarity.Rare, TargetType.Self) { }
    protected override async Task OnPlay(PlayerChoiceContext context, CardPlay play)
    {

        var effect = await PowerCmd.Apply<SpokenOmenPower>(context, Owner.Creature, DynamicVars["Damage"].BaseValue, Owner.Creature, this);
        if (effect is not null) effect.Block = DynamicVars["Block"].IntValue;
    }
    protected override void OnUpgrade()
    {
        DynamicVars["Damage"].UpgradeValueBy(1m);
        DynamicVars["Block"].UpgradeValueBy(2m);
    }
}
