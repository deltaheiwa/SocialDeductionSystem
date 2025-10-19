using Core.Enums;

namespace Core.Models;

public class StatusEffect
{
    /// <summary>
    /// The standardized, systemic effect type, if any.
    /// </summary>
    private SystemicEffectType? _systemicType { get; }
        
    /// <summary>
    /// The custom, role-specific effect type, if any.
    /// </summary>
    private string? _customType { get; }

    public int Duration { get; set; }
    // TODO: object is nono
    public object? Metadata { get; set; }

    /// <summary>
    /// Constructor for a SYSTEMIC effect.
    /// </summary>
    internal StatusEffect(SystemicEffectType type, int duration, object? metadata = null)
    {
        _systemicType = type;
        _customType = null;
        Duration = duration;
        Metadata = metadata;
    }

    /// <summary>
    /// Constructor for a CUSTOM effect.
    /// </summary>
    internal StatusEffect(string type, int duration, object? metadata = null)
    {
        if (string.IsNullOrWhiteSpace(type))
            throw new ArgumentNullException(nameof(type), "Custom effect type cannot be null or empty.");
            
        _systemicType = null;
        _customType = type;
        Duration = duration;
        Metadata = metadata;
    }

    /// <summary>
    /// Checks if this effect matches a systemic type.
    /// </summary>
    public bool Is(SystemicEffectType type)
    {
        return _systemicType == type;
    }

    /// <summary>
    /// Checks if this effect matches a custom type (case-insensitive).
    /// </summary>
    public bool Is(string type)
    {
        return string.Equals(_customType, type, StringComparison.OrdinalIgnoreCase);
    }

    /// <summary>
    /// Checks if this effect matches a role-specific effect type in the format "RoleName:EffectName" (case-insensitive).
    /// </summary>
    public bool Is(string roleName, string effectName) => Is($"{roleName}:{effectName}");
    
    public bool IsSystemic => _systemicType.HasValue;
    public bool IsCustom => _customType != null;
}