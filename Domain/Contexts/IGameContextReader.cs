using SocialDeductionSystem.Domain.Interfaces;
using SocialDeductionSystem.Domain.Interfaces.Player;
using SocialDeductionSystem.Domain.ValueObjects;
using SocialDeductionSystem.Domain.ValueObjects.Enums.Game;
using SocialDeductionSystem.Domain.ValueObjects.Enums.Player;
using SocialDeductionSystem.Domain.ValueObjects.Records;

namespace SocialDeductionSystem.Domain.Contexts;

public interface IGameContextReader
{
    GameId CurrentGameId { get; }
    ISdgVariationRuleset CurrentRuleset { get; }
    CommonPhaseType CurrentPhase { get; }
    int PhaseNumber { get; }
    
    Task<IPlayer?> GetPlayerByIdAsync(PlayerId playerId);
    Task<IEnumerable<IPlayer>> GetPlayersAsync(PlayerStatus? status = null);
    
    Task<IEnumerable<VoteRecord>> GetVotesForPhaseAsync(CommonPhaseType phaseType, int phaseNumber);
    Task<IEnumerable<ActionRecord>> GetActionsForPhaseAsync(CommonPhaseType phaseType, int phaseNumber);
    Task<IEnumerable<IPlayer>> GetVisitorsToPlayerAsync(PlayerId targetPlayerId, int dayNumber, CommonPhaseType phaseType);
}