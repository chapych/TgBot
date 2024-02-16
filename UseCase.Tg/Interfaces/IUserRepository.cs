using System.Linq.Expressions;
using TgBot.Entities.Entities;

namespace TgBot.UseCase.Interfaces;

public interface IUserRepository
{
    Task<TgUser> FindByChat(long chatId, params Expression<Func<TgUser, object>>[] included);
    Task SaveChangesAsync();
    void Add(long chatId);
}