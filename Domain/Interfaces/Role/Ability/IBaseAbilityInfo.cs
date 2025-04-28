using SocialDeductionSystem.Domain.ValueObjects;
using SocialDeductionSystem.Domain.ValueObjects.Enums.Role.Ability;

namespace SocialDeductionSystem.Domain.Interfaces.Role.Ability;

public interface IBaseAbilityInfo
{
    AbilityId Id { get; }
    string Name { get; }
    string Description { get; }
    AbilityCategoryType AbilityCategoryTypes { get; }
    AbilityTargetType AllowedTargetTypes { get; }
    AbilityReachType AllowedReachTypes { get; }
}