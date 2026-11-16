namespace LinneaMod.Content.Powers;

[RegisterPower]
public sealed class HardStudyPower : LinneaPowerBase
{
    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Counter;

    public override async Task AfterCardPlayed(PlayerChoiceContext context, CardPlay play)
    {
        if (play.Card.Owner.Creature == Owner && play.Card is Lumi)
            await LumiCompanion.Encourage(context, Owner.Player!, Amount, null);
    }
}
