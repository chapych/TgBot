using System.ComponentModel;
using TgBot.Models.EF.Users;

namespace UseCase.KudaGo
{
    public interface IKudaGoService
    {
        Task<List<Event>> GetEventsAsync(int count, Category[]? categories, DateTime date);
    }
}
