namespace Core.Interfaces;

public interface IExecutingIntent : IActionIntent
{
    bool Execute(GameState gameState);
}