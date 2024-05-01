using TgBot.Entities.Enums;
using TgBot.UseCase.Interfaces;
using TgBot.UseCase.States;

namespace TgBot.UseCase.Services;

public class StateMachine : IStateMachine
{
    private readonly IUserRepository _userRepository;
    private readonly IStateFactory _stateFactory;

    private long _chatId;
    private State _currentName = State.Start;

    private readonly List<StateTransition> _initialTransitions =
    [
        new StateTransition(State.Start,
            [State.Menu]),
        new StateTransition(State.Menu,
            [State.Search, State.Setting]),
        new StateTransition(State.Show, 
            [])
    ];

    public StateMachine(IUserRepository userRepository, IStateFactory stateFactory)
    {
        _userRepository = userRepository;
        _stateFactory = stateFactory;
    }

    public async Task ExecuteAsync(long chatId, string message)
    {
        _chatId = chatId;
        
        var user = await _userRepository.FindByChatOrDefaultAsync(_chatId);
        if (user != null)
            _currentName = user.State;

        var stateName = _stateFactory.CreateStateName(message);
        if (IsTransitionCausedByTextCommandAllowed(stateName))
        {
            await ChangeStateAsync(stateName);
        
            (await _userRepository.FindByChatOrDefaultAsync(chatId))
                .ChangeState(_currentName);
            await _userRepository.SaveChangesAsync();   
        }
    }

    public async Task ChangeStateAsync(State stateName)
    {
        _currentName = stateName;
            
        var state = _stateFactory.Create(_currentName);
        await state.EnterAsync(_chatId, this);
    }

    private bool IsTransitionCausedByTextCommandAllowed(State stateName)
    {
        return stateName == _currentName || 
               _initialTransitions.FirstOrDefault(t => t.From == _currentName)
                   ?.IsValid(stateName) == true;
    }
}