using Core.Enums;
using Core.Enums.Flags;
using Core.Models;

namespace Core.Interfaces;

public interface IRole
{
    string Name { get; }
    Faction Faction { get; }
    Subalignment Subalignment { get; }
    List<IAbility> Abilities { get; }
    RoleTags LogicalTags { get; }
    List<string> CosmeticTags { get; }
        
    void AssignOwnerToAbilities(Player owner);
}