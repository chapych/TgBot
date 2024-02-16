using TgBot.Entities.Entities;

namespace TgBot.UseCase.Interfaces;

public interface IKeyboardFactory
{
    KeyBoard CreateMainKeyboard();
    KeyBoard CreateSwipeKeyboard();
}