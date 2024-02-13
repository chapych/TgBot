using System.ComponentModel;
using Entities.Entities;

namespace UseCase.KudaGo
{
    public interface IKudaGoService
    {
        Task<List<Event>> GetEventsAsync(KudaGoRequest request);
    }
}
