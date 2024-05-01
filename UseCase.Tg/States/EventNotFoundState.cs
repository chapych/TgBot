using TgBot.UseCase.Interfaces;

namespace TgBot.UseCase.States;

public class EventNotFoundState : IState
{
    private readonly IMessageSender _messageSender;

    public EventNotFoundState(IMessageSender messageSender)
    {
        _messageSender = messageSender;
    }

    private const string NO_EVENTS_FOUND_TEXT = "События данной категории закончились :'(";

    public async Task EnterAsync(long chatId, IStateMachine stateMachine)
    {
        await _messageSender.SendTextMessageAsync(chatId, NO_EVENTS_FOUND_TEXT);
    }
}