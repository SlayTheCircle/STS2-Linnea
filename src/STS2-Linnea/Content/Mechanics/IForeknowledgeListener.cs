namespace LinneaMod.Content.Mechanics;

public interface IForeknowledgeListener
{
    Task AfterForeknowledgeGained(PlayerChoiceContext context, Player player, int amount);
    Task AfterForeknowledgeSpent(PlayerChoiceContext context, Player player, int amount);
}
