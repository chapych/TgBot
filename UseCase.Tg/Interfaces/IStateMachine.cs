using TgBot.Entities.Enums;
using TgBot.UseCase.States;

namespace TgBot.UseCase.Interfaces;

public interface IStateMachine
{
    Task ExecuteAsync(long chatId, string message);
    Task ChangeStateAsync(State stateName);
}