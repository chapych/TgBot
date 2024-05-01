using System.Collections;
using KudaGo.Entities.Entities;
using KudaGo.UseCases;
using TgBot.Entities.Entities;
using TgBot.Infrastructure.Extensions;
using TgBot.UseCase.Interfaces;

namespace TgBot.Infrastructure.Services
{
    using Category = Entities.Enums.Category;
    internal class EventLoader : IEventLoader
    {
        private readonly IKudaGoService _kudaGoService;
        private readonly IEventRepository _eventsRepository;

        public EventLoader(IKudaGoService kudaGoService, IEventRepository eventsRepository)
        {
            _kudaGoService = kudaGoService;
            _eventsRepository = eventsRepository;
        }

        public async Task<IEnumerable<Event>> LoadNonAddedEventsAsync(int count) => 
            await GetLoadedEvents([], count);

        public async Task<IEnumerable<Event>> LoadNonAddedEventsAsync(IEnumerable<Category> categories, int count) => 
            await GetLoadedEvents(categories, count);

        private async Task<List<Event>> GetLoadedEvents(IEnumerable<Category> categories, int count)
        {
            var result = new List<Event>(count);
            var kudaGoCategories = categories?
                .Select(x => x.ToKudaGoCategory())
                .ToArray();

            var pageIndex = 0;
            while (!IsLoadedEventsEnough(result, count))
            {
                var request = new KudaGoRequest
                {
                    Categories = kudaGoCategories,
                    Count = count - result.Count,
                    Date = DateTime.Today,
                    PageNumber = pageIndex++
                };
                var pageResult = await _kudaGoService.GetEventsAsync(request);
                var pageResultEvents = pageResult.Events;
                
                var nonAdded = GetNonAddedEvents(pageResultEvents);
                result.AddRange(nonAdded);
                
                if (HasRunOutOfAvailableEvents(pageResultEvents, request))
                    break;
            }

            return result;
        }

        private static bool IsLoadedEventsEnough(ICollection result, int count) => 
            result.Count >= count;

        private static bool HasRunOutOfAvailableEvents(IEnumerable<KudaGoEvent> pageResultEvents, 
            KudaGoRequest request) => 
            pageResultEvents.Count() < request.Count;

        private IEnumerable<Event> GetNonAddedEvents(IEnumerable<KudaGoEvent> pageResultEvents)
        {
            var domainEvents = GetConvertedToDomainEvents(pageResultEvents);
            var nonAdded = _eventsRepository.GetNonAdded(domainEvents);
            return nonAdded;
        }

        private static IEnumerable<Event> GetConvertedToDomainEvents(IEnumerable<KudaGoEvent> events) =>
            events
                .Select(kudaGoEvent => kudaGoEvent.ToDomainEvent());
    }
}
