using SocialDeductionSystem.Domain.Interfaces.Role;
using SocialDeductionSystem.Domain.ValueObjects;
using SocialDeductionSystem.Domain.ValueObjects.Enums.Player;

namespace SocialDeductionSystem.Domain.Interfaces.Player;

public interface IPlayer
{
    PlayerId Id { get; }
    string? ExternalId { get; }
    string DisplayName { get; }
    PlayerStatus Status { get; }
    
    IRole Role { get; }
    
    GameId GameId { get; }
}