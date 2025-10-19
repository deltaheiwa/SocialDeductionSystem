using Core.Models;

namespace Core.Interfaces;

public interface IInputProvider
{
    Task<Player> GetTargetAsync(Player actor, List<Player> validTargets);
    Task<string> GetFeedbackAsync(Player actor, string prompt);
}