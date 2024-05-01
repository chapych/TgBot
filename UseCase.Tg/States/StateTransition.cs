using TgBot.Entities.Enums;

namespace TgBot.UseCase.States;

internal class StateTransition
{
    public State From { get; init; }
    private readonly List<State> _to;
    private readonly List<State> _defaultTo = [State.Start];

    public StateTransition(State from, IEnumerable<State> to)
    {
        From = from;
        
        _to = to.ToList();
    }

    public bool IsValid(State to)
    {
        return _defaultTo.Contains(to) || _to.Contains(to);
    }
}