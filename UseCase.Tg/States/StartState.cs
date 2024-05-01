using TgBot.Entities.Enums;
using TgBot.UseCase.Interfaces;

namespace TgBot.UseCase.States;

public class StartState : IState
{
    private readonly IMessageSender _messageSender;
    private readonly IUserRepository _userRepository;

    public StartState(
        IMessageSender messageSender,
        IUserRepository userRepository)
    {
        _userRepository = userRepository;
        _messageSender = messageSender;
    }

    public async Task EnterAsync(long chatId, IStateMachine stateMachine)
    {
        await _messageSender.SendTypingAsync(chatId);
        var user = await _userRepository.FindByChatOrDefaultAsync(chatId);
        if (user == null)
            await RegisterUser(chatId);
        
        await stateMachine.ChangeStateAsync(State.Menu);
    }

    private Task RegisterUser(long chatId)
    {
        _userRepository.Add(chatId);
        return _userRepository.SaveChangesAsync();
    }
}