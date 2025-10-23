using Core;
using Core.Enums;
using Core.Interfaces;
using Core.Models;

namespace GameData.Intents;

public class ApplyEffectIntent : IActionIntent
{
    public Player Source { get; }
    public Player Target { get; }
    public int Priority { get; }
    public VisitType Visit { get; }
    
    private readonly SystemicEffectType? _systemicEffect;
    private readonly string? _customEffect;
    private readonly int _duration;
    private readonly IAbility _sourceAbility;

    /// <summary>
    /// Constructor for applying a SYSTEMIC effect.
    /// </summary>
    public ApplyEffectIntent(IAbility sourceAbility, Player target, SystemicEffectType effect, int priority, int duration = 1, VisitType visitType  = VisitType.Normal)
    {
        Source = sourceAbility.Owner ?? throw new InvalidOperationException("Ability owner cannot be null.");
        Target = target;
        Priority = priority;
        Visit = visitType;
        _systemicEffect = effect;
        _duration = duration;
        _sourceAbility = sourceAbility;
    }

    /// <summary>
    /// Constructor for applying a CUSTOM effect.
    /// </summary>
    public ApplyEffectIntent(IAbility sourceAbility, Player target, string effect, int priority, int duration = 1, VisitType visitType = VisitType.Normal)
    {
        Source = sourceAbility.Owner ?? throw new InvalidOperationException("Ability owner cannot be null.");
        Target = target;
        Priority = priority;
        Visit = visitType;
        _customEffect = effect;
        _duration = duration;
        _sourceAbility = sourceAbility;
    }


    public bool Execute(GameState gameState)
    {
        if (_systemicEffect == SystemicEffectType.Roleblock
            // && Target.Role?.Attributes.Any(a => a is RoleblockImmuneAttribute)
            )
        {
            return false;
        }
        
        if (_systemicEffect.HasValue)
        {
            Target.ApplyEffect(_systemicEffect.Value, _duration);
        }
        else if (!string.IsNullOrEmpty(_customEffect))
        {
            Target.ApplyEffect(_sourceAbility, _customEffect, _duration);
        }

        return true;
    }
}