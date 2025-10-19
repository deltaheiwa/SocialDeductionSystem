using Core.Enums;
using Core.Interfaces;

namespace Core.Models;

public class Player
{
    /// <summary>
    /// Name of the player. Can be an alias or a username.
    /// </summary>
    public string Name { get; set; }
    
    /// <summary>
    /// Role of the player.
    /// </summary>
    public IRole? Role { get; private set; }
    
    /// <summary>
    /// Track if the player is alive.
    /// </summary>
    public bool IsAlive { get; set; }
    
    /// <summary>
    /// List of active status effects.
    /// </summary>
    public List<StatusEffect> ActiveEffects { get; } = [];

    public Player(string name)
    {
        Name = name;
        IsAlive = true;
        Role = null;
    }
    
    /// <summary>
    /// Set the role of the player.
    /// </summary>
    public void AssignRole(IRole role) => Role = role;
    
    /// <summary>
    /// Apply a standardized, systemic effect to the player.
    /// </summary>
    public void ApplyEffect(SystemicEffectType type, int duration, object? metadata = null)
    {
        // TODO: Stacking validation, etc.
        var effect = new StatusEffect(type, duration, metadata);
        ActiveEffects.Add(effect);
    }

    /// <summary>
    /// Apply a custom, role-specific effect to the player.
    /// </summary>
    /// <remarks>
    /// This method automatically namespaces the effect to prevent clashes.
    /// </remarks>
    public void ApplyEffect(IAbility sourceAbility, string customEffectName, int duration, object? metadata = null)
    {
        if (sourceAbility.Owner.Role == null)
        {
            throw new InvalidOperationException("Ability must have an owner and role to apply an effect.");
        }
        
        string namespacedType = $"{sourceAbility.Owner.Role.Name}:{customEffectName}";
            
        var effect = new StatusEffect(namespacedType, duration, metadata);
        ActiveEffects.Add(effect);
    }
    
    public bool HasEffect(SystemicEffectType type) => ActiveEffects.Any(e => e.Is(type));
    public bool HasEffect(string type) => ActiveEffects.Any(e => e.Is(type));
    public bool HasEffect(string roleName, string effectName) => ActiveEffects.Any(e => e.Is(roleName, effectName));
}
