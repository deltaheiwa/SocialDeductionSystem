using Core.Enums;

namespace GameData.Base;

public abstract class NeutralRoleBase : RoleBase
{
    public override Faction Faction => Faction.Neutral;
}