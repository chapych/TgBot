using TgBot.Entities.Entities;

namespace TgBot.UseCase.Interfaces;

public interface IMessageSender
{
    Task SendTypingAsync(long chatId);
    Task SendTextMessageAsync(long chatId, string text, KeyBoard keyboard = null);
}