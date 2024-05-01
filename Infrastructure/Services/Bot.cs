using Telegram.Bot.Types;
using TgBot.Entities.Enums;
using TgBot.UseCase.Interfaces;

namespace TgBot.Infrastructure.Services;

internal class Bot : IBot
{
    private readonly IStateMachine _stateMachine;

    public Bot(IStateMachine stateMachine)
    {
        _stateMachine = stateMachine;
    }

    public Task HandleUpdate(Update update)
    {
        return update switch
        {
            { Message: { } message } => BotOnMessageReceived(message),
            { CallbackQuery: { } callbackQuery} => BotOnCallbackQueryReceived(callbackQuery),
            _ => throw new ArgumentOutOfRangeException(nameof(update), update, null)
        };
    }

    private async Task BotOnCallbackQueryReceived(CallbackQuery callbackQuery)
    {
        var chat = callbackQuery.Message!.Chat;
        if (chat == null)
            throw new ArgumentNullException(nameof(chat));
        var callbackQueryData = callbackQuery.Data;

        await _stateMachine.ExecuteAsync(chat.Id, callbackQueryData);
    }

    private async Task BotOnMessageReceived(Message message)
    {
        var chat = message.Chat;
        if (chat == null) 
            throw new ArgumentNullException(nameof(chat));
        var messageText = message.Text;

        await _stateMachine.ExecuteAsync(chat.Id, messageText);
    }
}