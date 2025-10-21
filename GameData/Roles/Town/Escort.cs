using Core.Enums;
using GameData.Base;

namespace GameData.Roles.Town;

public class Escort : TownRoleBase
{
    public override string Name => "Escort";
    public override Subalignment Subalignment => Subalignment.Support;
    
    public Escort() : base()
    {
        
    }
}

