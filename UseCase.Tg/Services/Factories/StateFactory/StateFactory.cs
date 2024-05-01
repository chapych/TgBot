using TgBot.Entities.Constants;
using TgBot.Entities.Enums;
using TgBot.UseCase.Interfaces;
using TgBot.UseCase.States;

namespace TgBot.UseCase.Services.Factories.StateFactory;

public class StateFactory : IStateFactory
{
    private readonly IUserRepository _userRepository;
    private readonly IKeyboardFactory _keyboardFactory;
    private readonly IMessageSender _messageSender;
    private readonly IEventLoader _eventLoader;
    private readonly IEventRepository _eventRepository;

    public StateFactory(IUserRepository userRepository,
        IKeyboardFactory keyboardFactory,
        IMessageSender messageSender, 
        IEventLoader eventLoader, 
        IEventRepository eventRepository)
    {
        _userRepository = userRepository;
        _keyboardFactory = keyboardFactory;
        _messageSender = messageSender;
        _eventLoader = eventLoader;
        _eventRepository = eventRepository;
    }
    
    public IState Create(State state)
    {
        return state switch
        {
            State.Start => new StartState(_messageSender, _userRepository),
            State.Menu => new MenuState(_messageSender, _keyboardFactory),
            State.Search => new SearchState(_messageSender, _userRepository, _eventLoader, _eventRepository),
            State.Show => new ShowState(_messageSender, _userRepository, _keyboardFactory),
            State.EventNotFound => new EventNotFoundState(_messageSender),
            _ => null
        };
    }

    public State CreateStateName(string @string)
    {
        return @string switch
        {
            Constants.CommandNames.Start => State.Start,
            Constants.CommandNames.Search => State.Search,
            _ => throw new ArgumentOutOfRangeException(nameof(@string), @string, null)
        };
    }
}