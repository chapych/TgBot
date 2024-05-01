using TgBot.UseCase.Enums;

namespace TgBot.UseCase.Interfaces;

public interface IState
{
    Task EnterAsync(long chatId, IStateMachine stateMachine);
}