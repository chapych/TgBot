using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TgBot.Entities.Entities;

namespace TgBot.Infrastructure.EF;

public class EventEntityTypeConfiguration : IEntityTypeConfiguration<Event>
{
    public void Configure(EntityTypeBuilder<Event> builder)
    {
        builder.Property(x => x.Id)
            .ValueGeneratedNever();
    }
}