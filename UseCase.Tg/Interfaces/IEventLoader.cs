using Entities.Interfaces;

namespace UseCase.Interfaces;

public interface IEventLoader
{
    Task<IEnumerable<IUserEvent>> LoadEventsAsync();
}