using TgBot.UseCase.Interfaces;

namespace TgBot.UseCase.States;

public class MenuState : IState
{
    private readonly IMessageSender _messageSender;
    private readonly IKeyboardFactory _keyboardFactory;

    public MenuState(IMessageSender messageSender, IKeyboardFactory keyboardFactory)
    {
        _keyboardFactory = keyboardFactory;
        _messageSender = messageSender;
    }

    public async Task EnterAsync(long chatId, IStateMachine stateMachine)
    {
        var inlineKeyboard = _keyboardFactory.CreateMainKeyboard();

        await _messageSender.SendTextMessageAsync(chatId, "Привет, друг!");
        await _messageSender.SendTextMessageAsync(chatId, "1. Поиск, куда сходить", inlineKeyboard);
    }
}