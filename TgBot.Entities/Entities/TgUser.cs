using TgBot.Entities.Entities.Base;
using TgBot.Entities.Enums;

namespace TgBot.Entities.Entities;

public class TgUser : BaseEntity<Guid>
{
    private readonly List<Event> _events;
    public long ChatId { get; init; }
    public State State { get; private set; }
    public IEnumerable<Event> Events => _events.AsReadOnly();


    public TgUser(long chatId)
    {
        ChatId = chatId;
        State = State.Start;

        _events = [];
    }

    public void AddEvent(Event @event) => _events.Add(@event);
    public void AddEvents(IEnumerable<Event> events) => _events.AddRange(events);
    public void RemoveEvent(Event @event) => _events.Remove(@event);
    public void ChangeState(State state) => State = state;
}