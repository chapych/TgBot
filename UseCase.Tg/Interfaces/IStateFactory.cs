using TgBot.UseCase.States;
using State = TgBot.Entities.Enums.State;

namespace TgBot.UseCase.Interfaces;

public interface IStateFactory
{
    IState Create(State state);
    State CreateStateName(string @string);
}