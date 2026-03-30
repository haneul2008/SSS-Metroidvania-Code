using System.Collections.Generic;

public class StateMachine<TEnum, TState> where TEnum : System.Enum where TState : State<TEnum, TState>
{
    protected Dictionary<TEnum, TState> _statePairs = new Dictionary<TEnum, TState>();
    public TState CurrentState { get; private set; }
    public TEnum CurrentEnum { get; private set; }

    protected Agent _agent;

    public virtual void Initialize(TEnum initEnum, Agent agent)
    {
        CurrentState = _statePairs.GetValueOrDefault(initEnum);
        CurrentState?.Enter();

        CurrentEnum = initEnum;

        _agent = agent;
    }

    public virtual void AddState(TEnum @enum, TState state) => _statePairs.Add(@enum, state);

    public virtual void ChangeState(TEnum newStateEnum)
    {
        if (!_agent.CanChangeState || _agent.IsDead) return;

        CurrentState?.Exit();
        
        CurrentState = _statePairs.GetValueOrDefault(newStateEnum);
        CurrentEnum = newStateEnum;

        CurrentState?.Enter();
    }
}
