using UnityEngine;

public class EnemyState : State<EnemyStateEnum, EnemyState>
{
    protected Enemy _enemy;
    protected bool _endTriggerCalled;

    public EnemyState(StateMachine<EnemyStateEnum, EnemyState> stateMachine, string animName, Agent agent) : base(stateMachine, animName, agent)
    {
        _enemy = agent as Enemy;
    }

    public void AnimationEndTrigger() => _endTriggerCalled = true;

    public override void Enter()
    {
        base.Enter();

        _endTriggerCalled = false;
    }
}
