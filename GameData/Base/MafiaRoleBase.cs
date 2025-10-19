using Core.Enums;
using Core.Interfaces;
using GameData.WinConditions;

namespace GameData.Base;

public abstract class MafiaRoleBase : RoleBase
{
    public override Faction Faction => Faction.Mafia;

    public override List<IWinCondition> WinConditions { get; }
        = [new FactionWinCondition(Faction.Mafia)];
}