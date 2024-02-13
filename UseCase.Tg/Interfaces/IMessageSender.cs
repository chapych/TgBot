using Entities.Entities;

namespace UseCase.Interfaces;

public interface IMessageSender
{
    Task SendTypingAsync(long chatId);
    Task SendTextMessageAsync(long chatId, string text, KeyBoard keyboard = null);
}