using Entities.Constants;
using Telegram.Bot.Types;
using UseCase.Commands;
using UseCase.Interfaces;

namespace UseCase.Services.Bot;

public class Bot : IBot
{
    private readonly Dictionary<string, ICommand> _commands;

    public Bot(
        IMessageSender messageSender,
        IKeyboardFactory factory, 
        IUserRepository userRepository,
        IEventLoader eventLoader)
    {
        _commands = new Dictionary<string, ICommand>
        {
            [Constants.CommandNames.Start] = new StartCommand(
                messageSender, factory, 
                userRepository),
            [Constants.CommandNames.Search] = new SearchEventsCommand(
                messageSender, factory, 
                userRepository, eventLoader)
        };
    }

    public async Task HandleUpdate(Update update)
    {
        var handler = update switch
        {
            { Message: { } message } => BotOnMessageReceived(message),
            { CallbackQuery: { } callbackQuery} => BotOnCallbackQueryReceived(callbackQuery),
            _ => throw new ArgumentOutOfRangeException(nameof(update), update, null)
        };
        await handler;
    }

    private async Task BotOnCallbackQueryReceived(CallbackQuery callbackQuery)
    {
        var chat = callbackQuery.Message!.Chat;
        if (chat == null)
            throw new ArgumentNullException(nameof(chat));
        var callbackQueryData = callbackQuery.Data;

        if (callbackQueryData != null && _commands.TryGetValue(callbackQueryData, out var command))
        {
            await command.ExecuteAsync(chat.Id);
        }
    }

    private async Task BotOnMessageReceived(Message message)
    {
        var chat = message.Chat;
        if (chat == null) 
            throw new ArgumentNullException(nameof(chat));
        var messageText = message.Text;

        if (messageText != null && _commands.TryGetValue(messageText, out var command))
        {
            await command.ExecuteAsync(chat.Id);
        }
    }
}