using TgBot.Entities.Entities;
using TgBot.UseCase.Enums;

namespace TgBot.UseCase.Interfaces;

public interface ICommand
{
    Task<Trigger> ExecuteAsync(long chatId);
}