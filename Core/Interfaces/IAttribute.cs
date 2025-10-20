using Core.Models;

namespace Core.Interfaces;

public interface IAttribute
{
    string Name { get; }
    Player? Owner { get; set; }

    /// <summary>
    /// Register a passive attribute's logic with the event bus.
    /// </summary>
    /// <remarks>
    /// Does not require an IInputProvider (!!!)
    /// </remarks>
    /// <param name="eventBus">Event bus to register to.</param>
    /// <param name="gameState">Game state object that tracks dynamic game information.</param>
    void Register(EventBus eventBus, GameState gameState);
}