using SocialDeductionSystem.Domain.Contexts;

namespace SocialDeductionSystem.Domain.Interfaces.Game;

public interface IPhaseLogic
{
    public void OnDayStart(IGameContextReader gameContext);
    
    public void OnNightStart(IGameContextReader gameContext);
    
    public void OnDayEnd(IGameContextReader gameContext);
    
    public void OnNightEnd(IGameContextReader gameContext);
}