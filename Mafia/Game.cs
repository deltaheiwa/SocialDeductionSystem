namespace Mafia;

public class Game
{
    /// <summary>
    /// List of players in the game.
    /// </summary>
    public List<Player> Players { get; private set; }

    public Game(List<string> playerNames, List<Role> rolesToAssign)
    {
        if (playerNames.Count != rolesToAssign.Count)
        {
            throw new ArgumentException("Player names and roles must be the same length.");
        }
        
        Players = playerNames.Select(name => new Player(name)).ToList();
        AssignRolesRandomly(rolesToAssign);
    }
    
    private void AssignRolesRandomly(List<Role> rolesToAssign)
    {
        var random = new Random();
        var shuffledRoles = rolesToAssign.OrderBy(_ => random.Next()).ToList();
        
        for (int i = 0; i < Players.Count; i++)
        {
            Players[i].AssignRole(shuffledRoles[i]);
        }
    }
}