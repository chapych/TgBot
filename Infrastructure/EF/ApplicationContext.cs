using Microsoft.EntityFrameworkCore;
using TgBot.Entities.Entities;

namespace TgBot.Infrastructure.EF;

public sealed class ApplicationContext : DbContext
{
    public DbSet<TgUser> Users { get; set; }
    public DbSet<Event> Events { get; set; }

    public ApplicationContext(DbContextOptions<ApplicationContext> options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfiguration(new TgUserEntityTypeConfiguration());
        builder.ApplyConfiguration(new EventEntityTypeConfiguration());
    }
}