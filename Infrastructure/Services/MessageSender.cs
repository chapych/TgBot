using Telegram.Bot;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using TgBot.Entities.Entities;
using TgBot.Infrastructure.Extensions;
using TgBot.UseCase.Interfaces;

namespace TgBot.Infrastructure.Services;

internal class MessageSender : IMessageSender
{
    private readonly ITelegramBotClient _botClient;

    public MessageSender(ITelegramBotClient botClient)
    {
        _botClient = botClient;
    }

    public Task SendTypingAsync(long chatId) =>
        _botClient.SendChatActionAsync(chatId, ChatAction.Typing);

    public Task SendTextMessageAsync(long chatId, string text, KeyBoard keyboard = null)
    {
        var markup = default(IReplyMarkup);
        if (keyboard != null)
        {
            markup = new ReplyKeyboardMarkup(keyboard.Buttons.ToKeyboardButtons())
            {
                OneTimeKeyboard = true,
                ResizeKeyboard = true
            };
        }

        return _botClient.SendTextMessageAsync(chatId, text: text, replyMarkup: markup);
    }
}