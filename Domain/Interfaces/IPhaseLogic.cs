namespace SocialDeductionSystem.Domain.Interfaces;

public interface IPhaseLogic
{
    public void OnDayStart(IGameContext gameContext);
    
    public void OnNightStart(IGameContext gameContext);
    
    public void OnDayEnd(IGameContext gameContext);
    
    public void OnNightEnd(IGameContext gameContext);
}