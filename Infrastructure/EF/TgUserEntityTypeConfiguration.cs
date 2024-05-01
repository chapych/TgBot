using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TgBot.Entities.Entities;

namespace TgBot.Infrastructure.EF;

public class TgUserEntityTypeConfiguration : IEntityTypeConfiguration<TgUser>
{
    public void Configure(EntityTypeBuilder<TgUser> builder)
    {
        builder.HasMany(e => e.Events)
        .WithMany(e => e.Users)
        .UsingEntity<TgUserEvent>(
            r => r.HasOne(e=>e.Event).WithMany().HasForeignKey(e => e.EventId),
            l => l.HasOne(e => e.TgUser).WithMany().HasForeignKey(e => e.TgUserId));
    }
}