using Microsoft.EntityFrameworkCore;
using Telegram.Bot.Types;

namespace TgBot.Models.EF;

public class ApplicationContext : DbContext
{
    public DbSet<User> Users { get; set; } = null!;

    public ApplicationContext(DbContextOptions<ApplicationContext> options)
        : base(options)
    {
    }
}