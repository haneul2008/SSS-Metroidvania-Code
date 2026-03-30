using UnityEngine;

public class GhoulWakeupState : EnemyState
{
    private Health health;
    private AgentRenderer _renderer;

    public GhoulWakeupState(StateMachine<EnemyStateEnum, EnemyState> stateMachine, string animName, Agent agent) : base(stateMachine, animName, agent)
    {
        health = _enemy.GetCompo<Health>();
        _renderer = _agent.GetCompo<AgentRenderer>();
    }

    public override void Enter()
    {
        base.Enter();

        _renderer.RightFlip(_enemy.targetTrm.position.x > _enemy.transform.position.x);
    }

    public override void Update()
    {
        base.Update();

        if (_endTriggerCalled)
        {
            _stateMachine.ChangeState(EnemyStateEnum.Chase);
        }
    }

    public override void Exit()
    {
        base.Exit();

        health.isCanHit = true;
    }
}
