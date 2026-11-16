using MegaCrit.Sts2.Core.Entities.Potions;
using LinneaMod.Content.PotionPools;

namespace LinneaMod.Content.Potions;

[RegisterPotion(typeof(LinneaPotionPool))]
public sealed class GemBerryJam : LinneaPotionBase
{
    public override PotionRarity Rarity => PotionRarity.Rare;
    public override PotionUsage Usage => PotionUsage.CombatOnly;
    public override TargetType TargetType => TargetType.Self;
    protected override IEnumerable<string> RegisteredKeywordIds => new[] { LinneaKeywords.Encourage };
    protected override async Task OnUse(PlayerChoiceContext context, Creature? target)
    {

        await PowerCmd.Apply<GemBerryJamPower>(context, Owner.Creature, 2, Owner.Creature, null);
        int emptySlots = Owner.PotionSlots.Count(p => p is null);
        for (int i = 0; i < emptySlots; i++)
            await PotionCmd.TryToProcure<MegaCrit.Sts2.Core.Models.Potions.PotionShapedRock>(Owner);
    }
}
