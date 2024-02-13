using UseCase.Interfaces;

namespace UseCase.Commands;

public class StartCommand : ICommand
{
    private readonly IMessageSender _messageSender;
    private readonly IKeyboardFactory _keyboardFactory;
    private readonly IUserRepository _userRepository;

    public StartCommand(
        IMessageSender messageSender,
        IKeyboardFactory keyboardFactory, 
        IUserRepository userRepository)
    {
        _keyboardFactory = keyboardFactory;
        _userRepository = userRepository;
        _messageSender = messageSender;
    }

    public async Task ExecuteAsync(long chatId)
    {
        await _messageSender.SendTypingAsync(chatId);

        var user = await _userRepository.FindByChat(chatId);
        if (user == null) 
            await RegisterUser(chatId);

        var inlineKeyboard = _keyboardFactory.CreateMainKeyboard();

        await _messageSender.SendTextMessageAsync(chatId, "Привет, друг!");
        await _messageSender.SendTextMessageAsync(chatId, "1. Поиск, куда сходить", inlineKeyboard);
    }

    private async Task RegisterUser(long chatId)
    {
        _userRepository.Add(chatId);
        await _userRepository.SaveChangesAsync();
    }
    
}