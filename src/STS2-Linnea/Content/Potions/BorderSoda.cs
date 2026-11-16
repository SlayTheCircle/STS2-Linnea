using MegaCrit.Sts2.Core.Entities.Potions;
using LinneaMod.Content.PotionPools;

namespace LinneaMod.Content.Potions;

[RegisterPotion(typeof(LinneaPotionPool))]
public sealed class BorderSoda : LinneaPotionBase
{
    public override PotionRarity Rarity => PotionRarity.Uncommon;
    public override PotionUsage Usage => PotionUsage.CombatOnly;
    public override TargetType TargetType => TargetType.Self;
    protected override IEnumerable<string> RegisteredKeywordIds => new[] { LinneaKeywords.Foreknowledge };
    protected override async Task OnUse(PlayerChoiceContext context, Creature? target)
    {
        await Foreknowledge.Gain(context, Owner, 14, null);
    }
}
