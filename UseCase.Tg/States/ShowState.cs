using TgBot.Entities.Entities;
using TgBot.Entities.Enums;
using TgBot.UseCase.Interfaces;

namespace TgBot.UseCase.States;

public class ShowState : IState
{
    private readonly IUserRepository _userRepository;
    private readonly IMessageSender _messageSender;
    private readonly IKeyboardFactory _keyboardFactory;
    
    public ShowState(IMessageSender messageSender,
        IUserRepository userRepository,
        IKeyboardFactory keyboardFactory)
    {
        _userRepository = userRepository;
        _messageSender = messageSender;
        _keyboardFactory = keyboardFactory;
    }

    public async Task EnterAsync(long chatId, IStateMachine stateMachine)
    {
        var user = await _userRepository.FindByChatOrDefaultAsync(chatId, x => x.Events);
        if (user != null)
        {
            var @event = user.Events.FirstOrDefault();

            if (@event == null)
            {
                await stateMachine.ChangeStateAsync(State.EventNotFound);
                return;
            }
            var info = GetShortInfo(@event);
            var swipeKeyboard = _keyboardFactory.CreateSwipeKeyboard();
            await _messageSender.SendTextMessageAsync(chatId, info, swipeKeyboard);
        }
    }
    
    private static string GetShortInfo(Event @event) =>
        string.Concat(@event.Name![0].ToString().ToUpper(),
            @event.Name.AsSpan(1)) + "\n" + @event.Description;
}