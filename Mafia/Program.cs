using Mafia.Roles;

namespace Mafia;

class Program
{
    static void Main(string[] args)
    {
        // 1. Define the players for this game.
        var playerNames = new List<string> { "Alice", "Bob", "Charlie", "David" };

        // 2. Define the role composition for this game (e.g., 1 Mafia, 3 Villagers).
        var roles = new List<Role>
        {
            new MafiaGoonRole(),
            new CitizenRole(),
            new CitizenRole(),
            new CitizenRole()
        };

        // 3. Create a new game instance. This will automatically assign roles.
        var game = new Game(playerNames, roles);

        Console.WriteLine("Game has started! Roles have been assigned secretly.");

        // 4. Let's print the assigned roles to verify it's working.
        //    (In a real game, you would only show each player their own role).
        foreach (var player in game.Players)
        {
            Console.WriteLine($"{player.Name} is a {player.Role.Name}");
        }
    }
}