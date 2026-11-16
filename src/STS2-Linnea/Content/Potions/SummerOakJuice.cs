using MegaCrit.Sts2.Core.Entities.Potions;
using LinneaMod.Content.PotionPools;

namespace LinneaMod.Content.Potions;

[RegisterPotion(typeof(LinneaPotionPool))]
public sealed class SummerOakJuice : LinneaPotionBase
{
    public override PotionRarity Rarity => PotionRarity.Common;
    public override PotionUsage Usage => PotionUsage.CombatOnly;
    public override TargetType TargetType => TargetType.Self;
    protected override IEnumerable<string> RegisteredKeywordIds => new[] { LinneaKeywords.Encourage };
    protected override async Task OnUse(PlayerChoiceContext context, Creature? target)
    {
        await LumiCompanion.Encourage(context, Owner, 9, null);
    }
}
