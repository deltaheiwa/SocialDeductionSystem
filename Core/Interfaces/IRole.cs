using Core.Enums;
using Core.Enums.Flags;
using Core.Models;

namespace Core.Interfaces;

public interface IRole
{
    string Name { get; }
    Faction Faction { get; }
    Subalignment Subalignment { get; }
    
    /// <summary>
    /// List of ACTIVE abilities.
    /// </summary>
    List<IAbility> Abilities { get; }
    
    /// <summary>
    /// List of PASSIVE abilities (denoted as ATTRIBUTES).
    /// </summary>
    List<IAttribute> Attributes { get; }
    List<IWinCondition> WinConditions { get; }
    RoleTags LogicalTags { get; }
    List<string> CosmeticTags { get; }
    DefenseType BaseDefense { get; }
    
    /// <summary>
    /// Called after a role gets assigned to the player.
    /// Responsible for setting player's initial state.
    /// </summary>
    /// <param name="player">Player object this role gets assigned to</param>
    void OnAssignment(Player player);
}