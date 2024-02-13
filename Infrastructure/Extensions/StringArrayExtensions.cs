using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types.ReplyMarkups;

namespace Infrastructure.Extensions;

public static class StringArrayExtensions
{
    public static KeyboardButton[] ToKeyboardButtons(this string[] buttons)
    {
        return buttons.Select(x => new KeyboardButton(x))
            .ToArray();
    }
}