using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Entities.Entities;
using Microsoft.EntityFrameworkCore;
using UseCase.Interfaces;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Infrastructure;

internal class UserRepository : IUserRepository
{
    private readonly ApplicationContext _context;

    public UserRepository(ApplicationContext context)
    {
        _context = context;
    }

    public async Task<User> FindByChat(long chatId, params Expression<Func<User, object>>[] included)
    {
        var query = _context.Users.AsQueryable();
        foreach (var includeProperty in included)
        {
            query = query.Include(includeProperty);
        }
        return await query.FirstOrDefaultAsync(x => x.ChatId == chatId);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }

    public void Add(long chatId)
    {
        _context.Users.Add(new User(chatId));
    }
}