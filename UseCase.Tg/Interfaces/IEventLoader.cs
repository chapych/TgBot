using TgBot.Entities.Interfaces;

namespace TgBot.UseCase.Interfaces;

public interface IEventLoader
{
    Task<IEnumerable<IUserEvent>> LoadEventsAsync();
}