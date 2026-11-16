namespace LinneaMod.Content.Cards;

/// <summary>观石知璞：按原案与升级表实现。</summary>
[RegisterCard(typeof(LinneaCardPool))]
public sealed class ReadTheStone : LinneaCardBase
{
    protected override IEnumerable<IHoverTip> AdditionalHoverTips => new IHoverTip[] { HoverTipFactory.FromCard<Ore>() };
    protected override IEnumerable<DynamicVar> CanonicalVars => new DynamicVar[] { new DamageVar(6m, ValueProp.Move), new CardsVar(1), new DynamicVar("ExtraDamage", 6m) };

    public ReadTheStone() : base(1, CardType.Attack, CardRarity.Common, TargetType.AnyEnemy) { }
    protected override async Task OnPlay(PlayerChoiceContext context, CardPlay play)
    {
        ArgumentNullException.ThrowIfNull(play.Target);
        await DamageCmd.Attack(DynamicVars.Damage.BaseValue).FromCard(this, play).Targeting(play.Target!).WithHitFx("vfx/vfx_attack_slash").Execute(context);
        await CardPileCmd.Draw(context, DynamicVars.Cards.BaseValue, Owner);
        var selected = (await CardSelectCmd.FromHandForDiscard(context, Owner, new CardSelectorPrefs(CardSelectorPrefs.DiscardSelectionPrompt, 1), null, this)).ToArray();
        await CardCmd.Discard(context, selected);
        if (selected.Any(c => c is Ore))
        {
        await DamageCmd.Attack(DynamicVars["ExtraDamage"].BaseValue).FromCard(this, play).TargetingAllOpponents(CombatState!).WithHitFx("vfx/vfx_attack_slash").Execute(context);
        }
    }
    protected override void OnUpgrade()
    {
        DynamicVars["Damage"].UpgradeValueBy(1m);
        DynamicVars["ExtraDamage"].UpgradeValueBy(3m);
    }
}
