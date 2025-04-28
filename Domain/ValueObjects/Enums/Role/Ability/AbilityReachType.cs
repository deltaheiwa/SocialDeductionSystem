namespace SocialDeductionSystem.Domain.ValueObjects.Enums.Role.Ability;

[Flags]
public enum AbilityReachType
{
    None = 0,
    Physical = 1 << 0, // 1
    Remote = 1 << 1, // 2
}