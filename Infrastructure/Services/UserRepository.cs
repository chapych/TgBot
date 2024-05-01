using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using TgBot.Entities.Entities;
using TgBot.Infrastructure.EF;
using TgBot.UseCase.Interfaces;

namespace TgBot.Infrastructure.Services;

internal class UserRepository : IUserRepository
{
    private readonly ApplicationContext _context;

    public UserRepository(ApplicationContext context)
    {
        _context = context;
    }

    public Task<TgUser> FindByChatOrDefaultAsync(long chatId, params Expression<Func<TgUser, object>>[] included)
    {
        var query = _context.Users.AsQueryable();
        query = included.Aggregate(query,
            (current, includeProperty) =>
                current.Include(includeProperty));

       return query.FirstOrDefaultAsync(x => x.ChatId == chatId);
    }

    public Task SaveChangesAsync() =>
        _context.SaveChangesAsync();

    public TgUser Add(long chatId)
    {
        var tgUserEFModel = new TgUser(chatId);
        _context.Users.Add(tgUserEFModel);

        return tgUserEFModel;
    }
}