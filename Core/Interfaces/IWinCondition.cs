using Core.Models;

namespace Core.Interfaces;

public interface IWinCondition
{
    /// <summary>
    /// Player that owns the win condition.
    /// </summary>
    Player? Owner { get; set; }

    /// <summary>
    /// Register the win condition with the event bus.
    /// </summary>
    /// <param name="eventBus">Event bus to register to.</param>
    /// <param name="gameState">Game state object that tracks dynamic game information.</param>
    void Register(EventBus eventBus, GameState gameState);
    
    /// <summary>
    /// Checks if this condition has been met to END the game.
    /// </summary>
    /// <returns>True if the game should end, otherwise false.</returns>
    bool CheckGameEnd(GameState gameState);
}