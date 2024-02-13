namespace UseCase.Interfaces;

public interface ICommand
{
    Task ExecuteAsync(long chatId);
}