namespace OrderProcessingSystem.Services;

public class EventBus
{
    private readonly Dictionary<Type, List<Func<object, Task>>> _handlers = new();

    public void Subscribe<T>(Func<T, Task> handler)
    {
        var eventType = typeof(T);

        if (!_handlers.ContainsKey(eventType))
        {
            _handlers[eventType] = new List<Func<object, Task>>();
        }

        _handlers[eventType]
            .Add(e => handler((T)e));
    }

    public async Task Publish<T>(T @event)
    {
        var eventType = typeof(T);

        if (!_handlers.ContainsKey(eventType))
            return;

        foreach (var handler in _handlers[eventType])
        {
            await handler(@event!);
        }
    }
}