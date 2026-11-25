using LinneaMod.Content.Cards;
using LinneaMod.Content.Powers;

namespace Sts2Mod.Foreseer;

internal static class LinneaForecasts
{
    public static void Subscribe(string modId)
        => ForeseerCardAdapter.Subscribe(modId, card => card is Lumi, Play);

    private static void Play(ForecastPlay forecast)
    {
        forecast.RequireSinglePlayWithOriginalHealth();
        var owner = forecast.Card.Owner.Creature;
        if (owner.HasPower<AllInPower>() || owner.HasPower<WeaknessSurveyPower>())
        {
            throw new ForecastUnavailable();
        }
        forecast.Attack(owner.HasPower<ContinuousHammerPower>() ? 2 : 1);
    }
}
