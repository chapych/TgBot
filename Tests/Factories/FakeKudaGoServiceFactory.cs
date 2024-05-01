using KudaGo.Entities.Entities;
using KudaGo.UseCases;
using TgBot.Entities.Entities;
using TgBot.Infrastructure.Extensions;

namespace TgBot.Infrastructure.Tests;

public static class FakeKudaGoServiceFactory
{
    public static IKudaGoService CreateFakeKudaGoService(IEnumerable<Event> fakeData) => 
        new FakeKudaGoService(fakeData);

    private class FakeKudaGoService : IKudaGoService
    {
        private readonly List<KudaGoEvent> _dataBase;

        public FakeKudaGoService(IEnumerable<Event> dataBase)
        {
            _dataBase = dataBase.Select(x => 
                    new KudaGoEvent(
                        x.Id, 
                        x.Name, 
                        x.Description,
                        [],
                        x.Categories
                        .Select(c => c.ToKudaGoCategory())))
                .ToList();
        }

        public Task<PageResult> GetEventsAsync(KudaGoRequest request)
        {
            var toReturn = _dataBase.Take(request.Count).ToList();
            _dataBase.RemoveRange(0, request.Count);
            return Task.FromResult(new PageResult
            {
                Events = toReturn, 
                PageIndex = request.PageNumber++
            });
        }
    }
}