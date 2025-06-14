using SocialDeductionSystem.Domain.Events;
using SocialDeductionSystem.Domain.Interfaces.Contexts;
using SocialDeductionSystem.Domain.Interfaces.Events;

namespace SocialDeductionSystem.Domain.Interfaces.Game;

public interface IGamePhase
{
    public void onPhaseStart(IPhaseContext context);
    
    public void onPhaseEnd(IPhaseContext context);
    
    public void HandleEvent(IPhaseContext context, IDomainEvent domainEvent);
    
    public IGamePhase GetNextPhase(IPhaseContext context);
}