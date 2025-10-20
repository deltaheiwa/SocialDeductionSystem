using Core.Enums;
using Core.Enums.Flags;
using Core.Interfaces;
using Core.Models;
using GameData.Validation;

namespace GameData.Base;

public abstract class RoleBase : IRole
{
    public abstract string Name { get; }
    public abstract Faction Faction { get; }
    public abstract Subalignment Subalignment { get; }

    public List<IAbility> Abilities { get; protected set; } = [];
    public List<IAttribute> Attributes { get; protected set; } = [];
    public abstract List<IWinCondition> WinConditions { get; }
    
    
    public virtual RoleTags LogicalTags { get; } = RoleTags.None;
    public virtual List<string> CosmeticTags { get; } = []; 


    protected RoleBase()
    {
        RoleValidator.Validate(Faction, Subalignment);
    }

    public void AssignOwnerToAbilities(Player owner)
    {
        foreach (var ability in Abilities)
            ability.Owner = owner;
    }

    public void AssignOwnerToAttributes(Player owner)
    {
        foreach (var attribute in Attributes)
            attribute.Owner = owner;
    }

    public void AssignOwnerToWinConditions(Player owner)
    {
        foreach (var winCondition in WinConditions)
            winCondition.Owner = owner;
    }
}