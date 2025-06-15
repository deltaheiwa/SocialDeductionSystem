using SocialDeductionSystem.Domain.Interfaces.Role;
using SocialDeductionSystem.Domain.ValueObjects;

namespace SocialDeductionSystem.Domain.Interfaces.External.Factories;

/// <summary>
/// Singleton factory interface for creating instances of IRole.
/// </summary>
public interface IRoleFactory
{
    /// <summary>
    /// Creates an instance of the concrete class implementing IRole
    /// corresponding to the provided RoleId, potentially applying
    /// variation-specific adapters.
    /// </summary>
    /// <param name="roleId">The unique string identifier for the base role (e.g., "Werewolf").</param>
    /// <param name="variationInfo">Information about the current SDG variation.</param>
    /// <returns>An instance implementing IRole (could be base role or an adapter).</returns>
    /// <exception cref="ArgumentException">Thrown if the roleId is unknown.</exception>
    /// <exception cref="InvalidOperationException">Thrown if role is incompatible with variation.</exception>
    IRole CreateRole(RoleId roleId, ISdgVariationRuleset variationInfo);
    
    /// <summary>
    /// Gets all the RoleIds that are associated with a specific VariationId.
    /// </summary>
    IEnumerable<RoleId> GetRolesForVariation(VariationId variationId);
}