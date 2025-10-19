namespace Core.Events;

public class DayStartEvent(int dayNumber) : GameEventBase
{
    public int DayNumber { get; } = dayNumber;
}