using SocialDeductionSystem.Domain.ValueObjects;
using SocialDeductionSystem.Domain.ValueObjects.Enums.Role;

namespace SocialDeductionSystem.Domain.Interfaces.Role;

public interface ITeam
{
    TeamId TeamId { get; }
    
    string Name { get; }
    
    CommonAlignmentType Alignment { get; }
}