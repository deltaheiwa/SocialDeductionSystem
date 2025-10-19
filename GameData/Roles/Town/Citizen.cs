using Core.Enums;
using GameData.Base;

namespace GameData.Roles.Town;

public class Citizen : TownRoleBase
{
    public override string Name => "Citizen";
    public override Subalignment Subalignment => Subalignment.Citizen;
}