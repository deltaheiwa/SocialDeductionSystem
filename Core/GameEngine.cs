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

    private readonly HashSet<IWinCondition> _winConditions = new();

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
                ability.Register(_eventBus, _gameState, _inputProvider);
            
            
            foreach (var attribute in player.Role?.Attributes ?? [])
                attribute.Register(_eventBus, _gameState);
            
            foreach (var winCondition in player.Role?.WinConditions ?? [])
            {
                winCondition.Register(_eventBus, _gameState);
                
                _winConditions.Add(winCondition);
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
            foreach (var winCondition in _winConditions)
            {
                if (winCondition.CheckGameEnd(_gameState))
                {
                    _gameState.CurrentPhase = GamePhase.GameOver;
                    break;
                }
            }
        }
        
        var gameOverEvent = new GameOverEvent();
        
        await _eventBus.BroadcastAsync(gameOverEvent);
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
        var orderedIntents = _gameState.NightIntents
            .OrderBy(intent => intent.Priority)
            .ToList();
        
        foreach (var modifier in orderedIntents.OfType<IModifyingIntent>())
        {
            modifier.Modify(_gameState);
        }
        
        _gameState.VisitLogs.Clear();
        var visitIntents = orderedIntents.Where(
            i => i.Visit != VisitType.None && i.Target != null);
        
        foreach (var intent in visitIntents)
        {
            _gameState.VisitLogs.Add(new VisitLog(
                intent.Source, 
                intent.Target!, 
                0,
                intent.Visit
            ));
        }
        
        foreach (var action in orderedIntents.OfType<IExecutingIntent>())
        {
            action.Execute(_gameState);
        }
    }
}