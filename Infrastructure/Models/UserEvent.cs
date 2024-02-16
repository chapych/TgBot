using KudGo.Entities.Entitites;
using TgBot.Entities.Interfaces;

namespace TgBot.Infrastructure.Models
{
    internal class UserEvent : IUserEvent
    {
        public string Name { get; }
        public string Description { get; }

        public UserEvent(Event kudaGoEvent)
        {
            Name = kudaGoEvent.Name;
            Description = kudaGoEvent.Description;
        }
    }
}
