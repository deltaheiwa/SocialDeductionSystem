using Core.Enums;
using Core.Models;

namespace Core.Interfaces;

public interface IAbility
{
    string Name { get; }
    Player Owner { get; set; }
    AbilityType Type { get; }
    
    /// <summary>
    /// Register the ability to the event bus.
    /// Passive abilities should register to relevant events they are triggered by.
    /// Active abilities should register to the NightStart event and use IInputProvider to get input.
    /// </summary>
    /// <param name="eventBus">Event bus to register to</param>
    /// <param name="gameState">Game state object that tracks dynamic game information</param>
    /// <param name="inputProvider">Input provider for getting player input</param>
    void Register(EventBus eventBus, GameState gameState, IInputProvider inputProvider);
}