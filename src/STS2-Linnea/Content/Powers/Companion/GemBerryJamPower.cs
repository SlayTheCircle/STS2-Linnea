namespace LinneaMod.Content.Powers;

[RegisterPower]
public sealed class GemBerryJamPower : LinneaPowerBase
{
    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Counter;
    public override async Task AfterPotionUsed(PotionModel potion, Creature? target)
    {
        if (potion.Owner.Creature == Owner && potion is MegaCrit.Sts2.Core.Models.Potions.PotionShapedRock)
            await LumiCompanion.Encourage(new ThrowingPlayerChoiceContext(), potion.Owner, Amount, null);
    }
}
