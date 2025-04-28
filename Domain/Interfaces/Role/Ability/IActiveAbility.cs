using SocialDeductionSystem.Domain.ValueObjects;
using SocialDeductionSystem.Domain.ValueObjects.Enums.Role.Ability;

namespace SocialDeductionSystem.Domain.Interfaces.Role.Ability;

public interface IActiveAbility : IBaseAbilityInfo
{
    /// <summary>
    /// Specific flag for abilities like "Remote-House" that require
    /// knowing extra information (like house location) even for remote use.
    /// </summary>
    bool RequiresTargetLocationKnowledge { get; } // Handles "Remote-House" nuance
    
    PermittedActivationPhases PermittedPhases { get; }
}