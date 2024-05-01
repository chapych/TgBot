using TgBot.Entities.Entities;
using TgBot.Entities.Enums;
using TgBot.Infrastructure.Services;
using static Tests.TestContextFactory.TestContextFactory;

namespace TgBot.Infrastructure.Tests;

public class EventRepositoryTests
{
    [Test]
    public async Task AddOne_Successful()
    {
        await using var context = CreateDbContextForTesting();

        var eventRepository = new EventRepository(context);

        var @event = new Event(0, "name", "description", [Category.BusinessEvents]);

        var expected = eventRepository.Add(@event);

        var actual = await context.Events.FindAsync(expected.Id);

        Assert.Multiple(() =>
        {
            Assert.That(actual, Is.Not.Null);
            Assert.That(actual?.Categories, Does.Contain(Category.BusinessEvents));
        });
    }

    [Test]
    public async Task AddMany_Successful()
    {
        await using var context = CreateDbContextForTesting();

        var eventRepository = new EventRepository(context);

        var event1 = new Event(1, "name", "description", [Category.BusinessEvents]);
        var event2 = new Event(2, "name", "description", [Category.BusinessEvents]);

        eventRepository.AddRange([event1, event2]);

        await eventRepository.SaveChangesAsync();

        var events = context.Events;

        Assert.That(events.Count(), Is.EqualTo(2));
    }


    [Test]
    public async Task GetNonLiked_Successful()
    {
        await using var context = CreateDbContextForTesting();

        var eventRepository = new EventRepository(context);
        var userRepository = new UserRepository(context);

        var event1 = new Event(1, "name", "description", [Category.BusinessEvents]);
        var event2 = new Event(2, "name", "description", [Category.BusinessEvents]);

        eventRepository.AddRange([event1, event2]);

        await eventRepository.SaveChangesAsync();

        var tgUser = userRepository.Add(1);
        tgUser.AddEvents([event1, event2]);

        await eventRepository.SaveChangesAsync();

        await eventRepository.MarkLikedAsync(tgUser, event1);
        
        await eventRepository.SaveChangesAsync();

        var nonShown = await eventRepository.GetNonLikedToUserByCategoryAsync(tgUser, [Category.BusinessEvents], 10);

        Assert.Multiple(() =>
        {
            Assert.That(nonShown.Count(), Is.EqualTo(1));
            Assert.That(nonShown.First().Equals(event2), Is.True);
        });
    }

    [Test]
    public async Task GetNonAdded()
    {
        await using var context = CreateDbContextForTesting();

        var eventRepository = new EventRepository(context);
        var userRepository = new UserRepository(context);

        var event1 = new Event(1, "name", "description", [Category.BusinessEvents]);
        var event2 = new Event(2, "name", "description", [Category.BusinessEvents]);

        eventRepository.Add(event1);

        await eventRepository.SaveChangesAsync();

        IEnumerable<Event> nonAdded = eventRepository.GetNonAdded([event1, event2]);

        Assert.Multiple(() =>
        {
            Assert.That(nonAdded.Count(), Is.EqualTo(1));
            Assert.That(nonAdded.First().Equals(event2), Is.True);
        });
    }

    
}