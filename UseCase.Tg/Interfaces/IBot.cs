using Telegram.Bot.Types;

namespace UseCase.Interfaces;

public interface IBot
{
    Task HandleUpdate(Update update);
}