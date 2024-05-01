using KudGo.Entities.Enums;
using TgBot.Entities.Entities;

namespace TgBot.UseCase.Interfaces;

using Category = Entities.Enums.Category;
public interface IEventLoader
{
    Task<IEnumerable<Event>> LoadNonAddedEventsAsync(int count);
    Task<IEnumerable<Event>> LoadNonAddedEventsAsync(IEnumerable<Category> categories, int count);
}