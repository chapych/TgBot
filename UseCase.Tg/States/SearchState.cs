using TgBot.Entities.Entities;
using TgBot.Entities.Enums;
using TgBot.UseCase.Interfaces;

namespace TgBot.UseCase.States;

public class SearchState : IState
{
    private readonly IUserRepository _userRepository;
    private readonly IEventLoader _eventLoader;
    private readonly IMessageSender _messageSender;
    private readonly IEventRepository _eventRepository;

    private const int MAX_COUNT = 10;

    public SearchState(
        IMessageSender messageSender,
        IUserRepository userRepository,
        IEventLoader eventLoader,
        IEventRepository eventRepository)
    {
        _userRepository = userRepository;
        _eventLoader = eventLoader;
        _eventRepository = eventRepository;
        _messageSender = messageSender;
    }

    public async Task EnterAsync(long chatId, IStateMachine stateMachine)
    {
        await _messageSender.SendTypingAsync(chatId);

        await _messageSender.SendTextMessageAsync(chatId, Entities.Constants.Constants.Emoji.Eyes);

        var user = await _userRepository.FindByChatOrDefaultAsync(chatId, x => x.Events);
        if (user != null)
        {
            if (!user.Events.Any())
            {
                Category[] requiredCategories = [Category.BusinessEvents];

                var preLoadedEvents = await GetNonLikedLoadedEvents(user, requiredCategories);
                user.AddEvents(preLoadedEvents);
                
                var currentCount = preLoadedEvents.Count;
                if (currentCount < MAX_COUNT)
                {
                    var events = (await _eventLoader.LoadNonAddedEventsAsync(requiredCategories,
                            MAX_COUNT - currentCount))
                        .ToList();

                    _eventRepository.AddRange(events);

                    await _eventRepository.SaveChangesAsync();

                    user.AddEvents(events);
                }
            }

            await stateMachine.ChangeStateAsync(State.Show);
        }
    }

    private async Task<List<Event>> GetNonLikedLoadedEvents(TgUser user, Category[] requiredCategories)
    {
        return (await _eventRepository.GetNonLikedToUserByCategoryAsync(user,
                requiredCategories,
                MAX_COUNT))
            .ToList();
    }
}