using Core;
using Core.Enums;
using Core.Events;
using Core.Interfaces;
using Core.Models;
using GameData.Base;

namespace GameData.Roles.SharedAbilities;

public class SimpleRoleblockAbility : IAbility
{
    public string Name => "Roleblock";
    public Player? Owner { get; set; }
    
    public void Register(EventBus eventBus, GameState gameState, IInputProvider inputProvider)
    {
        eventBus.Subscribe<NightStartEvent>(async nightStartEvent =>
        {
            if (Owner == null || !Owner.IsAlive)
                return;
            
            // TODO: Implement roleblock
        });
    }
}