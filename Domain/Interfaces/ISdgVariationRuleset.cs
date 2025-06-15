using SocialDeductionSystem.Domain.Configurations;
using SocialDeductionSystem.Domain.Interfaces.Contexts;
using SocialDeductionSystem.Domain.Interfaces.Game;
using SocialDeductionSystem.Domain.Interfaces.Player;
using SocialDeductionSystem.Domain.ValueObjects;
using SocialDeductionSystem.Domain.ValueObjects.Enums.Game;

namespace SocialDeductionSystem.Domain.Interfaces;

public interface ISdgVariationRuleset
{
    VariationId Id { get; }
    string Name { get; }


    IEnumerable<RoleId> GetAvailableRoleIds();

    WinConditionResult CheckWinConditions(IGameContextReader gameContext);
    
    IGamePhase GetInitialPhase();

    /// <summary>
    /// Checks if a role is compatible with the variation's rules.
    /// </summary>
    bool IsRoleCompatible(RoleId roleId);

    /// <summary>
    /// Checks if a proposed game setup is valid according to the variation's rules.
    /// </summary>
    bool IsSetupValid(GameSetup setup);

    /// <summary>
    /// Assigns roles to players based on the specific setup for this game session.
    /// </summary>
    void AssignRoles(List<IPlayer> players, GameSetup setup);
}