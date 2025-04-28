namespace SocialDeductionSystem.Domain.Interfaces.Role.Ability;

public interface IPassiveAbility : IBaseAbilityInfo
{
    IReadOnlySet<Type> TriggeringEventTypes { get; }
}