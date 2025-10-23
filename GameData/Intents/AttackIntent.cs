using Core;
using Core.Enums;
using Core.Interfaces;
using Core.Models;

namespace GameData.Intents;

public class AttackIntent : IActionIntent
{
    public Player Source { get; }
    public Player Target { get; }
    public int Priority { get; }
    public VisitType Visit { get; }
    
    public AttackType Type { get; }
    
    public AttackIntent(Player source, Player target, AttackType attackType, int priority = 10, VisitType visitType = VisitType.Normal)
    {
        Source = source;
        Target = target;
        Type = attackType;
        Priority = priority;
        Visit = visitType;
    }
    
    public bool Execute(GameState gameState)
    {
        throw new NotImplementedException();
    }
}