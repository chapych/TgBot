namespace TgBot.UseCase.Interfaces;

public interface ICommand
{
    Task ExecuteAsync(long chatId);
}