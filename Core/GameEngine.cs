using Core.Enums;
using Core.Events;
using Core.Interfaces;
using Core.Models;

namespace Core;

public class GameEngine
{
    private readonly GameState _gameState;
    private readonly EventBus _eventBus;
    private readonly IInputProvider _inputProvider;

    public GameEngine(List<string> playerNames, List<IRole> roles, IInputProvider inputProvider)
    {
        _gameState = new GameState();
        _eventBus = new EventBus();
        _inputProvider = inputProvider;

        InitializePlayers(playerNames, roles);
    }

    private void InitializePlayers(List<string> playerNames, List<IRole> roles)
    {
        // a real one would shuffle roles.
        // TODO: Add role shuffling
        for (int i = 0; i < playerNames.Count; i++)
        {
            var player = new Player(playerNames[i]);
            player.AssignRole(roles[i]);
            _gameState.Players.Add(player);
        }

        foreach (var player in _gameState.Players)
        {
            foreach (var ability in player.Role?.Abilities ?? [])
            {
                ability.Register(_eventBus, _gameState, _inputProvider);
            }
        }

        _gameState.CurrentPhase = GamePhase.Day;
    }

    public async Task RunGame()
    {
        while (_gameState.CurrentPhase != GamePhase.GameOver)
        {
            switch (_gameState.CurrentPhase)
            {
                case GamePhase.Day:
                    await RunDayPhase();
                    break;
                case GamePhase.Night:
                    await RunNightPhase();
                    break;
            }
            CheckWinConditions();
        }
    }

    private async Task RunDayPhase()
    {
        await _eventBus.BroadcastAsync(new DayStartEvent(_gameState.DayNumber));
        
        // TODO: Add discussion and voting logic
        _gameState.CurrentPhase = GamePhase.Night;
    }

    private async Task RunNightPhase()
    {
        // clear previous night's actions
        _gameState.NightIntents.Clear();

        // announce the start of the night
        await _eventBus.BroadcastAsync(new NightStartEvent(_gameState.DayNumber));

        // process the registered intents
        // this is where the complex pipeline of:
        //    - Target Modification
        //    - Action Execution
        //    - Feedback Generation
        //    - Feedback Modification
        //    - Cleanup
        // would happen.
        await ProcessNightActions();

        // move to the next day
        _gameState.DayNumber++;
        _gameState.CurrentPhase = GamePhase.Day;
    }

    private async Task ProcessNightActions()
    {
        // Priority resolution, etc.
        throw new NotImplementedException();
    }

    private void CheckWinConditions()
    {
        var livingPlayers = _gameState.Players.Where(p => p.IsAlive).ToList();
        var livingMafia = livingPlayers.Count(p => p.Role.Faction == Faction.Mafia);
        var livingTown = livingPlayers.Count(p => p.Role.Faction == Faction.Town);

        if (livingMafia == 0)
        {
            _gameState.CurrentPhase = GamePhase.GameOver;
        }
        else if (livingMafia >= livingTown)
        {
            _gameState.CurrentPhase = GamePhase.GameOver;
        }
    }
}