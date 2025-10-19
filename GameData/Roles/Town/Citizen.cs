using Core.Enums;
using GameData.Base;

namespace GameData.Roles.Town;

public class Citizen : RoleBase
{
    public override string Name => "Citizen";
    public override Faction Faction => Faction.Town;
    public override Subalignment Subalignment => Subalignment.Citizen;
}