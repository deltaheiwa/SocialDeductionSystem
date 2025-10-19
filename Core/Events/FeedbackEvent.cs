using Core.Models;

namespace Core.Events;

public class FeedbackEvent(Player targetPlayer, string message) : GameEventBase
{
    public Player TargetPlayer { get; } = targetPlayer;
    public string Message { get; set; } = message;
    public bool IsModified { get; set; } = false;
}