using Core.Events;

namespace Core;

public class EventBus
{
    private readonly Dictionary<Type, List<Func<GameEventBase, Task>>> _subscribers = new();

    public void Subscribe<T>(Func<T, Task> action) where T : GameEventBase
    {
        Type eventType = typeof(T);
        if (!_subscribers.ContainsKey(eventType))
        {
            _subscribers[eventType] = new List<Func<GameEventBase, Task>>();
        }

        // wrap the specific action (Action<T>) in a more general one (Func<GameEventBase, Task>)
        _subscribers[eventType].Add(async (gameEvent) => await action((T)gameEvent));
    }

    public async Task BroadcastAsync(GameEventBase gameEvent)
    {
        Type eventType = gameEvent.GetType();
        if (_subscribers.ContainsKey(eventType))
        {
            foreach (var action in _subscribers[eventType])
            {
                await action(gameEvent);
            }
        }
    }
}