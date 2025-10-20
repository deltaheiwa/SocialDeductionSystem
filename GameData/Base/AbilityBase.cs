using Core;
using Core.Enums;
using Core.Interfaces;
using Core.Models;

namespace GameData.Base;

public abstract class AbilityBase : IAbility
{
    public abstract string Name { get; }
    public abstract Player? Owner { get; set; }
    public abstract void Register(EventBus eventBus, GameState gameState, IInputProvider inputProvider);
}