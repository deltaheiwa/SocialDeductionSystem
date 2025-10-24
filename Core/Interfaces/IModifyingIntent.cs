namespace Core.Interfaces;

public interface IModifyingIntent : IActionIntent
{
    void Modify(GameState gameState);
}