using UnityEngine;

public class EnemyHitState : EnemyState
{
    public EnemyHitState(StateMachine<EnemyStateEnum, EnemyState> stateMachine, string animName, Agent agent) : base(stateMachine, animName, agent)
    {
    }

    public override void Update()
    {
        base.Update();

        if (_endTriggerCalled)
        {
            ChangeState();
        }
    }

    protected virtual void ChangeState()
    {
        _stateMachine.ChangeState(EnemyStateEnum.Idle);
    }
}
