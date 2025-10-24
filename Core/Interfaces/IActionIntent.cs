using Core.Enums;
using Core.Models;

namespace Core.Interfaces;

public interface IActionIntent
{
    /// <summary>
    /// The player who initiated the action.
    /// </summary>
    Player Source { get; }
    
    /// <summary>
    /// The player targeted by the action.
    /// </summary>
    Player? Target { get; }
    
    /// <summary>
    /// The priority of the action. Higher priority actions are executed first.
    /// </summary>
    /// <remarks>
    /// 0 is the highest priority.
    /// </remarks>
    int Priority { get; }
    
    /// <summary>
    /// The visit this action performs when executed.
    /// </summary>
    VisitType Visit { get; }
}