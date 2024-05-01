using TgBot.Entities.Entities;
using TgBot.Entities.Enums;
using TgBot.Infrastructure.Services;
using static Tests.TestContextFactory.TestContextFactory;


namespace TgBot.Infrastructure.Tests
{
    public class UserRepositoryTests
    {
        [Test]
        public async Task AddOne_Successful()
        {
            await using var context = CreateDbContextForTesting();

            var userRepository = new UserRepository(context);

            var chatId = 000001;

            var tgUser = userRepository.Add(chatId);

            await userRepository.SaveChangesAsync();

            var user = context.Users.FindAsync(tgUser.Id);

            Assert.IsNotNull(user);
        }
        
        [Test]
        public async Task AddMany_Successful()
        {
            await using var context = CreateDbContextForTesting();

            var userRepository = new UserRepository(context);

            var initial = 000001;
            int max = 10;
            
            for (int i = 0; i < max; i++) 
                userRepository.Add(initial++);
            
            await userRepository.SaveChangesAsync();

            var users= context.Users;

            Assert.That(users.Count(), Is.EqualTo(max));
        }

        [Test]
        public async Task FindAsync_Successful()
        {
            await using var context = CreateDbContextForTesting();

            var userRepository = new UserRepository(context);

            var chatId = 000001;

            var tgUser = userRepository.Add(chatId);

            await userRepository.SaveChangesAsync();

            var actual = await userRepository.FindByChatOrDefaultAsync(chatId);

            Assert.That(actual, Is.EqualTo(tgUser));
        }
        
        [Test]
        public async Task FindAsync_ShouldReturnNullIfNotFound()
        {
            await using var context = CreateDbContextForTesting();

            var userRepository = new UserRepository(context);

            var chatId = 000001;

            userRepository.Add(chatId);

            await userRepository.SaveChangesAsync();

            var actual = await userRepository.FindByChatOrDefaultAsync(++chatId);

            Assert.That(actual, Is.EqualTo(null));
        }

        [Test]
        public async Task FindAsyncWithIncludedProperties_Successful()
        {
            await using var context = CreateDbContextForTesting();

            var userRepository = new UserRepository(context);

            var chatId = 000001;

            var tgUser = userRepository.Add(chatId);

            tgUser.AddEvent(new Event(1, "name", "description", new [] {Category.BusinessEvents}));

            await userRepository.SaveChangesAsync();

            var actualUser = await userRepository.FindByChatOrDefaultAsync(chatId, x => x.Events);

            var actualUserLoadedEvent = actualUser.Events.First();

            Assert.That(actualUserLoadedEvent.Name, Is.EqualTo("name"));
        }
    }
}