namespace Core.Events;

public class NightStartEvent(int nightNumber) : GameEventBase
{
    public int NightNumber { get; } = nightNumber;
}