using SocialDeductionSystem.Domain.Configurations;
using SocialDeductionSystem.Domain.Interfaces;
using SocialDeductionSystem.Domain.Interfaces.Game;
using SocialDeductionSystem.Domain.Interfaces.Player;
using SocialDeductionSystem.Domain.ValueObjects;
using SocialDeductionSystem.Domain.ValueObjects.Enums.Game;

namespace SocialDeductionSystem.Domain.Models;

public class Game
{
    private readonly List<IPlayer> _players = new();
    private IGamePhase _currentPhase;

    public GameId Id { get; private set; }
    public GameStatus Status { get; private set; }
    public ISdgVariationRuleset Ruleset { get; }
    public GameSetup Setup { get; }
    
    public static Game CreateNew(ISdgVariationRuleset ruleset, GameSetup setup)
    {
        if (!ruleset.IsSetupValid(setup))
        {
            throw new InvalidOperationException("The provided game setup is not valid for this variation.");
        }
        
        var game = new Game(ruleset, setup);
        
        // TBD
        
        return game;
    }
    
    private Game(ISdgVariationRuleset ruleset, GameSetup setup)
    {
        Ruleset = ruleset;
        Setup = setup;
    }
}