using Core.Enums;
using Core.Models;

namespace Core.Interfaces;

public interface IRole
{
    string Name { get; }
    Faction Faction { get; }
    Subalignment Subalignment { get; }
    List<IAbility> Abilities { get; }
        
    void AssignOwnerToAbilities(Player owner);
}