using Core;
using Core.Enums;
using Core.Events;
using Core.Interfaces;
using Core.Models;

namespace GameData.WinConditions;

public class FactionWinCondition : IWinCondition
{
    public required Player Owner { get; set; }
    private Faction _faction { get; }
    
    public FactionWinCondition(Faction faction)
    {
        _faction = faction;
    }
    
    public void Register(EventBus eventBus, GameState gameState)
    {
        eventBus.Subscribe<GameOverEvent>(async gameOverEvent =>
        {
            if (CheckGameEnd(gameState))
            {
                gameState.WinningFactions.Add(_faction);
            }
        });
    }

    public bool CheckGameEnd(GameState gameState)
    {
        var livingPlayers = gameState.Players.Where(p => p.IsAlive).ToList();
        var livingMafia = livingPlayers.Count(p => p.Role?.Faction == Faction.Mafia);
        var livingTown = livingPlayers.Count(p => p.Role?.Faction == Faction.Town);
        
        if (_faction == Faction.Mafia)
        {
            return livingMafia > 0 && livingMafia >= livingTown;
        }
        if (_faction == Faction.Town)
        {
            return livingMafia == 0;
        }
        
        return false;
    }
}