using SocialDeductionSystem.Domain.Interfaces.Game;
using SocialDeductionSystem.Domain.Interfaces.Role.Ability;
using SocialDeductionSystem.Domain.ValueObjects;

namespace SocialDeductionSystem.Domain.Interfaces.Role;

public interface IRole
{
    RoleId RoleId { get; }
    string Name { get; }
    string Description { get; }

    IReadOnlySet<IPassiveAbility> PassiveAbilities { get; }
    IReadOnlySet<IActiveAbility> ActiveAbilities { get; }
    
    ITeam Team { get; }
}