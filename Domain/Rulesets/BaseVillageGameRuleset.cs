using SocialDeductionSystem.Domain.Configurations;
using SocialDeductionSystem.Domain.Interfaces;
using SocialDeductionSystem.Domain.Interfaces.Contexts;
using SocialDeductionSystem.Domain.Interfaces.Game;
using SocialDeductionSystem.Domain.Interfaces.Player;
using SocialDeductionSystem.Domain.ValueObjects;
using SocialDeductionSystem.Domain.ValueObjects.Enums.Game;

namespace SocialDeductionSystem.Domain.Rulesets;

public abstract class BaseVillageGameRuleset : ISdgVariationRuleset
{
    public abstract VariationId Id { get; }
    public abstract string Name { get; }

    public abstract IEnumerable<RoleId> GetAvailableRoleIds();
    
    public virtual WinConditionResult CheckWinConditions(IGameContextReader gameContext)
    {
        throw new NotImplementedException();
    }

    public virtual IGamePhase GetInitialPhase()
    {
        throw new NotImplementedException();
    }

    public abstract bool IsRoleCompatible(RoleId roleId);

    public abstract bool IsSetupValid(GameSetup setup);

    public abstract void AssignRoles(List<IPlayer> players, GameSetup setup);
}