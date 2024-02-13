using Entities.Entities;

namespace UseCase.Interfaces;

public interface IKeyboardFactory
{
    KeyBoard CreateMainKeyboard();
    KeyBoard CreateSwipeKeyboard();
}