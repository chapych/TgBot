using Telegram.Bot.Types.ReplyMarkups;

namespace TgBot.Infrastructure.Extensions;

public static class StringArrayExtensions
{
    public static KeyboardButton[] ToKeyboardButtons(this string[] buttons)
    {
        return buttons.Select(x => new KeyboardButton(x))
            .ToArray();
    }
}