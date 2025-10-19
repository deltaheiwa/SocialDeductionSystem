using Core.Interfaces;

namespace Core.Models;

public class Player
{
    /// <summary>
    /// Name of the player. Can be an alias or a username.
    /// </summary>
    public string Name { get; set; }
    
    /// <summary>
    /// Role of the player.
    /// </summary>
    public IRole? Role { get; private set; }
    
    /// <summary>
    /// Track if the player is alive.
    /// </summary>
    public bool IsAlive { get; set; }

    public Player(string name)
    {
        Name = name;
        IsAlive = true;
        Role = null;
    }
    
    /// <summary>
    /// Set the role of the player.
    /// </summary>
    /// <param name="role">The role to assign</param>
    public void AssignRole(IRole role) => Role = role;
}