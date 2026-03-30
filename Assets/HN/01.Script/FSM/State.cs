using System;
using UnityEngine;

public class State<TEnum, TState> where TEnum : System.Enum where TState : State<TEnum, TState>
{
    protected int _animHash;
    protected Agent _agent;
    protected StateMachine<TEnum, TState> _stateMachine;

    public State(StateMachine<TEnum, TState> stateMachine, string animName, Agent agent)
    {
        _stateMachine = stateMachine;
        _animHash = Animator.StringToHash(animName);
        _agent = agent;
    }

    public virtual void Enter()
    {
        _agent.Anim.SetBool(_animHash, true);
    }

    public virtual void Update()
    {

    }

    public virtual void FixedUpdate()
    {

    }

    public virtual void Exit()
    {
        _agent.Anim.SetBool(_animHash, false);
    }
}
