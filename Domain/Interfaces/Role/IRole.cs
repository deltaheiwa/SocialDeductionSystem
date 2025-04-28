using SocialDeductionSystem.Domain.Interfaces.Game;
using SocialDeductionSystem.Domain.Interfaces.Role.Ability;

namespace SocialDeductionSystem.Domain.Interfaces.Role;

public interface IRole : IPhaseLogic
{
    string Name { get; }
    string Description { get; }

    IReadOnlySet<IPassiveAbility> PassiveAbilities { get; }
    IReadOnlySet<IActiveAbility> ActiveAbilities { get; }
    
    
}