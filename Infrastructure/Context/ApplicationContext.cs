using Microsoft.EntityFrameworkCore;
using TgBot.Entities.Entities;

namespace TgBot.Infrastructure.Context;

public sealed class ApplicationContext : DbContext
{
    public DbSet<TgUser> Users;

    public ApplicationContext(DbContextOptions<ApplicationContext> options)
        : base(options)
    {
    }

    public ApplicationContext()
    {

    }
}