namespace SocialDeductionSystem.Domain.ValueObjects.Enums.Role.Ability;

[Flags]
public enum AbilityCategoryType
{
    Lethal = 1 << 0, // 1
    Informative = 1 << 1, // 2
    Protective = 1 << 2, // 4
    Manipulative = 1 << 3, // 8
    Blocking = 1 << 4, // 16
    Other = 1 << 5, // 32
}