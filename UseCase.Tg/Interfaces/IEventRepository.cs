using KudaGo.Entities.Entities;
using TgBot.Entities.Entities;
using TgBot.Entities.Enums;

namespace TgBot.UseCase.Interfaces;

public interface IEventRepository
{
    Task SaveChangesAsync();
    Event Add(Event @event);
    IEnumerable<Event> AddRange(IEnumerable<Event> events);
    Task<IEnumerable<Event>> GetNonLikedToUserByCategoryAsync(TgUser user, Category[] categories, int max);
    Task MarkShownAsync(TgUser user, Event @event);
    IEnumerable<Event> GetNonAdded(IEnumerable<Event> events);
    Task MarkLikedAsync(TgUser user, Event @event);
}