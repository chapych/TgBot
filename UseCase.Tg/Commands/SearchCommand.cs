using Entities.Interfaces;
using UseCase.Interfaces;

namespace UseCase.Commands;

public class SearchEventsCommand : ICommand
{
    private readonly IKeyboardFactory _keyboardFactory;
    private readonly IUserRepository _userRepository;
    private readonly IEventLoader _eventLoader;
    private readonly IMessageSender _messageSender;

    public SearchEventsCommand(
        IMessageSender messageSender,
        IKeyboardFactory keyboardFactory,
        IUserRepository userRepository, 
        IEventLoader eventLoader)
    {
        _keyboardFactory = keyboardFactory;
        _userRepository = userRepository;
        _eventLoader = eventLoader;
        _messageSender = messageSender;
    }

    public async Task ExecuteAsync(long chatId)
    {
        await _messageSender.SendTypingAsync(chatId);

        await _messageSender.SendTextMessageAsync(chatId, Entities.Constants.Constants.Emoji.Eyes);

        var user = await _userRepository.FindByChat(chatId, x => x.LoadedEvents);
        if (user != null)
        {
            if (!user.LoadedEvents.Any())
            {
                var events = await _eventLoader.LoadEventsAsync();
                user.AddLoadedEvents(events);
            }

            var @event = user.LoadedEvents.First();
            var info = GetShortInfo(@event);

            user.RemoveEvent(@event);
            await _userRepository.SaveChangesAsync();

            var swipeKeyboard = _keyboardFactory.CreateSwipeKeyboard();
            await _messageSender.SendTextMessageAsync(chatId, info, swipeKeyboard);
        }
    }

    private static string GetShortInfo(IUserEvent @event) =>
        string.Concat(@event.Name![0].ToString().ToUpper(), 
            @event.Name.AsSpan(1)) + "\n" + @event.Description;
}