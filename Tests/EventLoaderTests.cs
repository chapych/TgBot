using TgBot.Entities.Entities;
using TgBot.Entities.Enums;
using TgBot.Infrastructure.Services;
using static Tests.TestContextFactory.TestContextFactory;

namespace TgBot.Infrastructure.Tests
{
    public class EventLoaderTests
    {
        [Test]
        public async Task LoadEvents_AlreadyPartlyStored()
        {
            //Arrange
            const int count = 2;
            await using var context = CreateDbContextForTesting();
            var event1 = new Event(1, "name1", "description1", [Category.BusinessEvents]);
            var event2 = new Event(2, "name2", "description2", [Category.BusinessEvents]);
            var event3 = new Event(3, "name3", "description3", [Category.BusinessEvents]);
            var event4 = new Event(4, "name4", "description4", [Category.BusinessEvents]);
            var event5 = new Event(5, "name5", "description5", [Category.BusinessEvents]);

            List<Event> events = [event1, event2, event3, event4, event5];

            var eventRepository = new EventRepository(context);
            var kudaGoService = FakeKudaGoServiceFactory.CreateFakeKudaGoService(events);
            var eventLoader = new EventLoader(kudaGoService, eventRepository);

            //Act
            eventRepository.AddRange(events.Take(events.Count - count));
            await eventRepository.SaveChangesAsync();

            var loadEventsAsync = await eventLoader.LoadNonAddedEventsAsync( count);

            //Assert
            Assert.Multiple(() =>
            {
                var actual = loadEventsAsync.ToList();
                
                Assert.That(actual, Has.Count.EqualTo(count));
                for (var i = count - 1; i >= actual.Count; i--) 
                    Assert.That(actual, Does.Contain(events[i]));
            });
        }
        
    }
}
