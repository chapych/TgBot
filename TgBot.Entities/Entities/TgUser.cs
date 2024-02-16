using TgBot.Entities.Interfaces;

namespace TgBot.Entities.Entities;

public class TgUser
{
    public Guid Id { get; private set; }
    public long ChatId { get; private set; }

    private readonly List<IUserEvent> _loadedEvents;
    public IEnumerable<IUserEvent> LoadedEvents => _loadedEvents.AsReadOnly();

    public TgUser(long chatId)
    {
        ChatId = chatId;
    
        _loadedEvents = new List<IUserEvent>();
    }

    public void AddLoadedEvent(IUserEvent @event) => _loadedEvents.Add(@event);
    public void AddLoadedEvents(IEnumerable<IUserEvent> events) => _loadedEvents.AddRange(events);
    public void RemoveEvent(IUserEvent @event) => _loadedEvents.Remove(@event);
}