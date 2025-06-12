using SocialDeductionSystem.Domain.ValueObjects;

namespace SocialDeductionSystem.Domain.Events;

public class VotePlacedEvent : IDomainEvent
{
    public PlayerId VoterId { get; }
    public PlayerId TargetId { get; }
    
    public VotePlacedEvent(PlayerId voterId, PlayerId targetId)
    {
        VoterId = voterId;
        TargetId = targetId;
    }
}