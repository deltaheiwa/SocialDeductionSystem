using Core.Enums;
using Core.Models;

namespace Core;

public class GameState
{
    public List<Player> Players { get; } = new List<Player>();
    public int DayNumber { get; set; } = 1;
    public GamePhase CurrentPhase { get; set; } = GamePhase.Setup;
    public List<VisitLog> VisitLogs { get; } = new List<VisitLog>();

    // This is for persistent effects like the Permatransporter's lines
    public Dictionary<string, object> PersistentEffects { get; } = new();
        
    // This is for actions that are registered during the night
    // I will need to define IActionIntent later, but for now I can just use a generic object store.
    public List<object> NightIntents { get; } = new List<object>();

    public Player? GetPlayer(string name) => Players.Find(p => p.Name == name);
}