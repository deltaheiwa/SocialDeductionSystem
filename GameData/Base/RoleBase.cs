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
    
    public virtual DefenseType BaseDefense { get; } = DefenseType.None;
    
    public void OnAssignment(Player player)
    {
        player.Defense = BaseDefense;
        
        AssignOwnerToAbilities(player);
        AssignOwnerToAttributes(player);
        AssignOwnerToWinConditions(player);
    }
    
    protected RoleBase()
    {
        RoleValidator.Validate(Faction, Subalignment);
    }

    protected void AssignOwnerToAbilities(Player owner)
    {
        foreach (var ability in Abilities)
            ability.Owner = owner;
    }

    protected void AssignOwnerToAttributes(Player owner)
    {
        foreach (var attribute in Attributes)
            attribute.Owner = owner;
    }

    protected void AssignOwnerToWinConditions(Player owner)
    {
        foreach (var winCondition in WinConditions)
            winCondition.Owner = owner;
    }
}