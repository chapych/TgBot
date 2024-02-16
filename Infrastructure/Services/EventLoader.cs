using KudaGo.UseCases;
using KudGo.Entities.Enums;
using TgBot.Entities.Interfaces;
using TgBot.Infrastructure.Models;
using TgBot.UseCase.Interfaces;

namespace TgBot.Infrastructure.Services
{
    internal class EventLoader : IEventLoader
    {
        private readonly IKudaGoService _kudaGoService;

        public EventLoader(IKudaGoService kudaGoService)
        {
            _kudaGoService = kudaGoService;
        }

        public async Task<IEnumerable<IUserEvent>> LoadEventsAsync()
        {
            var request = new KudaGoRequest
            {
                Categories = new[] { Category.BusinessEvents, Category.Cinema },
                Count = 10,
                Date = DateTime.Today
            };
            var kudaGoEvents =  await _kudaGoService.GetEventsAsync(request);

            return kudaGoEvents
                .Select(kudaGoEvent => new UserEvent(kudaGoEvent))
                .ToList();
        }
    }
}
