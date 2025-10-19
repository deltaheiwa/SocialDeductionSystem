using Core.Enums;
using Core.Interfaces;
using GameData.WinConditions;

namespace GameData.Base;

public abstract class TownRoleBase : RoleBase
{
    public override Faction Faction => Faction.Town;

    public override List<IWinCondition> WinConditions { get; }
        = [new FactionWinCondition(Faction.Town)];
}