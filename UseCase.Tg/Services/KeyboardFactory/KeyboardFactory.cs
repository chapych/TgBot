using TgBot.Entities.Constants;
using TgBot.Entities.Entities;
using TgBot.UseCase.Interfaces;

namespace TgBot.UseCase.Services.KeyboardFactory;

internal class KeyboardFactory : IKeyboardFactory
{
    public KeyBoard CreateMainKeyboard()
    {
        var buttons = new[]
        {
            Constants.CommandNames.Search
        };
        return new KeyBoard(buttons);
    }

    public KeyBoard CreateSwipeKeyboard()
    {
        var buttons = new[]
        {
            Constants.CommandNames.Like,
            Constants.CommandNames.Dislike,
        };

        return new KeyBoard(buttons);
    }
}