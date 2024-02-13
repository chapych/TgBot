using Entities.Constants;
using Entities.Entities;
using UseCase.Interfaces;

namespace UseCase.Services.KeyboardFactory;

public class KeyboardFactory : IKeyboardFactory
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