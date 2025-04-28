namespace SocialDeductionSystem.Domain.ValueObjects.Enums.Role.Ability;

[Flags]
public enum AbilityTargetType
{
    None = 0,
    Player = 1 << 0, // 1
    House = 1 << 1, // 2
    Self = 1 << 2, // 4
}