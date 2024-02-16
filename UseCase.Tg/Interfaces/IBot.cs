using Telegram.Bot.Types;

namespace TgBot.UseCase.Interfaces;

public interface IBot
{
    Task HandleUpdate(Update update);
}