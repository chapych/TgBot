using Microsoft.EntityFrameworkCore;
using TgBot.Entities.Entities;
using TgBot.Entities.Enums;
using TgBot.Infrastructure.EF;
using TgBot.UseCase.Interfaces;

namespace TgBot.Infrastructure.Services
{
    internal class EventRepository : IEventRepository
    {
        private readonly ApplicationContext _context;

        public EventRepository(ApplicationContext context)
        {
            _context = context;
        }

        public Task SaveChangesAsync() => 
            _context.SaveChangesAsync();

        public Event Add(Event @event)
        {
            _context.Events.Add(@event);

            return @event;
        }

        public IEnumerable<Event> AddRange(IEnumerable<Event> events)
        {
            var list = events.ToList();

            _context.Events.AddRange(list);

            return list;
        }

        public Task<IEnumerable<Event>> GetNonLikedToUserByCategoryAsync(TgUser user, Category[] categories, int max)
        {
            var userId = user.Id;
            var tgUserEvents = _context.Set<TgUserEvent>()
                .Where(x => x.TgUserId == userId)
                .ToList();

            var events = _context.Events
                .AsEnumerable()
                .Where(e =>
                {
                    var isTargetCategory = e.Categories.Any(categories.Contains);
                    if (!isTargetCategory)
                        return false;

                    var tgUserEvent = tgUserEvents.SingleOrDefault(x => x.EventId == e.Id);
                    return tgUserEvent?.Liked != true;
                })
                .Distinct()
                .Take(max);
            
            return Task.FromResult(events);
        }

        public async Task MarkShownAsync(TgUser user, Event @event)
        {
            var userEvent= await _context.Set<TgUserEvent>()
                .SingleOrDefaultAsync(ue => ue.TgUserId == user.Id && ue.EventId == @event.Id);
            userEvent?.MarkShown();
        }

        public async Task MarkLikedAsync(TgUser user, Event @event)
        {
            var userEvent= await _context.Set<TgUserEvent>()
                .SingleOrDefaultAsync(ue => ue.TgUserId == user.Id && ue.EventId == @event.Id);
            userEvent.MarkLiked();
        }

        public IEnumerable<Event> GetNonAdded(IEnumerable<Event> events) => 
            events.Where(x => !_context.Events.Contains(x));
    }
}
