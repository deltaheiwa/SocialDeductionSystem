namespace SocialDeductionSystem.Domain.ValueObjects.Enums.Role.Ability;

[Flags]
public enum PermittedActivationPhases
{
    Day = 1 << 0, // 1
    Night = 1 << 1, // 2
    Any = Day | Night, // 3
}