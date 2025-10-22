using Core.Models;

namespace Core.Interfaces;

public interface IActionIntent
{
    /// <summary>
    /// The player who initiated the action.
    /// </summary>
    Player Source { get; }
    
    /// <summary>
    /// The player targeted by the action.
    /// </summary>
    Player Target { get; }
    
    /// <summary>
    /// The priority of the action. Higher priority actions are executed first.
    /// 0 is the highest priority.
    /// </summary>
    int Priority { get; }
    
    /// <summary>
    /// Executes the action intent within the given game state.
    /// </summary>
    /// <param name="gameState">Game state object that tracks dynamic game information.</param>
    /// <returns>True if the action was executed successfully, otherwise false.</returns>
    bool Execute(GameState gameState);
}