namespace LinneaMod.Content.Cards;

/// <summary>万全准备：按原案与升级表实现。</summary>
[RegisterCard(typeof(LinneaCardPool))]
public sealed class FullPreparation : LinneaCardBase
{
    protected override IEnumerable<IHoverTip> AdditionalHoverTips => new IHoverTip[] { HoverTipFactory.FromCard<Lumi>() };
    public override IEnumerable<CardKeyword> CanonicalKeywords
    {
        get
        {
            HashSet<CardKeyword> result = new() {  };
            LinneaKeywords.AddTo(result, LinneaKeywords.Encourage);
            return result;
        }
    }

    public FullPreparation() : base(1, CardType.Skill, CardRarity.Rare, TargetType.Self) { }
    protected override async Task OnPlay(PlayerChoiceContext context, CardPlay play)
    {

        await LumiCompanion.Encourage(context, Owner, Owner.Creature.Block, this);
        if (LumiCompanion.Find(Owner) is { Pile.Type: PileType.Hand } lumi) lumi.EnableForTurn();
        else await LumiCompanion.Recall(context, Owner);
    }
    protected override void OnUpgrade()
    {
        EnergyCost.UpgradeBy(-1);
    }
}
