using SocialDeductionSystem.Domain.ValueObjects;

namespace SocialDeductionSystem.Domain.Interfaces;

public interface ISdgVariationRuleset
{
    VariationId Id { get; }
    string Name { get; }
    Version Version { get; }


    IEnumerable<RoleId> GetAvailableRoleIds();
    bool IsRoleCompatible(RoleId roleId);
    // RoleDistributionPlan GetRoleDistribution(int playerCount);
}