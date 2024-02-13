using Entities.Entities;
using System.Linq.Expressions;

namespace UseCase.Interfaces;

public interface IUserRepository
{
    Task<User> FindByChat(long chatId, params Expression<Func<User, object>>[] included);
    Task SaveChangesAsync();
    void Add(long chatId);
}