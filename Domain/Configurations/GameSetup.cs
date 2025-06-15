using SocialDeductionSystem.Domain.ValueObjects;

namespace SocialDeductionSystem.Domain.Configurations;

public record GameSetup
{
    public int PlayerCount { get; init; }
    public required List<RoleId> RolesToInclude { get; init; }
    
    public Dictionary<string, bool> OptionalRules { get; init; } = new();
}