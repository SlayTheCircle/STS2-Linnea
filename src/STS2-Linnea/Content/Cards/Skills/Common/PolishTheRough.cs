namespace LinneaMod.Content.Cards;

/// <summary>打磨璞玉：按原案与升级表实现。</summary>
[RegisterCard(typeof(LinneaCardPool))]
public sealed class PolishTheRough : LinneaCardBase
{
    protected override IEnumerable<DynamicVar> CanonicalVars => new DynamicVar[] { new DamageVar(6m, ValueProp.Move), new DynamicVar("ExtraDamage", 8m) };

    public PolishTheRough() : base(1, CardType.Skill, CardRarity.Common, TargetType.AllEnemies) { }
    protected override async Task OnPlay(PlayerChoiceContext context, CardPlay play)
    {
        await DamageCmd.Attack(DynamicVars.Damage.BaseValue).FromCard(this, play).TargetingAllOpponents(CombatState!).WithHitFx("vfx/vfx_attack_slash").Execute(context);
        if (!LinneaCards.Hand(Owner).Any(c => c.Type is CardType.Status or CardType.Curse)) return;
        var selected = await CardSelectCmd.FromHand(context, Owner, new CardSelectorPrefs(SelectionScreenPrompt, 1),
            c => c.Type is CardType.Status or CardType.Curse, this);
        foreach (var card in selected)
        {
            if (!await LinneaCardExhaust.Try(context, card)) continue;
            await DamageCmd.Attack(DynamicVars["ExtraDamage"].BaseValue).FromCard(this, play).TargetingAllOpponents(CombatState!).WithHitFx("vfx/vfx_attack_slash").Execute(context);
        }
    }
    protected override void OnUpgrade()
    {
        DynamicVars["Damage"].UpgradeValueBy(2m);
    }
}
