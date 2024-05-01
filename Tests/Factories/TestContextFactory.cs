using Microsoft.EntityFrameworkCore;
using TgBot.Infrastructure.EF;

namespace Tests.TestContextFactory
{
    public static class TestContextFactory
    {
        public static ApplicationContext CreateDbContextForTesting()
        {
            var options = new DbContextOptionsBuilder<ApplicationContext>()
                .UseInMemoryDatabase(databaseName: "TestTelegramUsers")
                .Options;

            var dbContext = new ApplicationContext(options);

            dbContext.Database.EnsureDeleted();
            dbContext.Database.EnsureCreated();

            return dbContext;
        }
    }

}
