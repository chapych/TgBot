using KudaGo.Entities.Entities;
using TgBot.Entities.Entities;

namespace TgBot.Infrastructure.Extensions;

public static class KudaGoEventExtensions
{
    public static Event ToDomainEvent(this KudaGoEvent kudaGoEvent)
    {
        return new Event(kudaGoEvent.Id, kudaGoEvent.Name, kudaGoEvent.Description,
            kudaGoEvent.Categories?.Select(x => x.ToDomainCategory()));
    }
}