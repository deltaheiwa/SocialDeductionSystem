using Core.Enums;
using Core.Interfaces;
using Core.Models;

namespace Core;

public class GameState
{
    public List<Player> Players { get; } = [];
    public int DayNumber { get; set; } = 1;
    public GamePhase CurrentPhase { get; set; } = GamePhase.Setup;
    public List<VisitLog> VisitLogs { get; } = [];

    // This is for persistent effects like the Permatransporter's lines
    public Dictionary<string, object> PersistentEffects { get; } = new();
    
    public List<IActionIntent> NightIntents { get; } = [];

    /// <summary>
    /// Winners of the game.
    /// </summary>
    /// <value>
    /// Mutable list of <c>Player</c> objects.
    /// </value>
    /// <remarks>
    /// Handles faction and solo winners.
    /// </remarks>
    public HashSet<Player> Winners { get; } = [];
    
    /// <summary>
    /// Factions that have won the game.
    /// </summary>
    /// <remarks>
    /// Technically, this set will only ever contain one element. But for the sake of extensibility in the future,
    /// I will keep it as a set.
    /// </remarks>
    public HashSet<Faction> WinningFactions { get; } = [];
    
    public Player? GetPlayer(string name) => Players.Find(p => p.Name == name);
}