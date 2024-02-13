using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Entities;
using Infrastructure.Extensions;
using Telegram.Bot;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using UseCase.Interfaces;

namespace Infrastructure;

internal class MessageSender : IMessageSender
{
    private readonly ITelegramBotClient _botClient;

    public MessageSender(ITelegramBotClient botClient)
    {
        _botClient = botClient;
    }

    public async Task SendTypingAsync(long chatId) => 
        await _botClient.SendChatActionAsync(chatId, ChatAction.Typing);

    public async Task SendTextMessageAsync(long chatId, string text, KeyBoard keyboard = null)
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

        await _botClient.SendTextMessageAsync(chatId, text: text, replyMarkup: markup);
    }
}