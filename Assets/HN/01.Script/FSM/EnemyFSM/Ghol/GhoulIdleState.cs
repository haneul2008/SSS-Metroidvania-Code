using UnityEngine;

public class GhoulIdleState : EnemyIdleState
{
    public GhoulIdleState(StateMachine<EnemyStateEnum, EnemyState> stateMachine, string animName, Agent agent) : base(stateMachine, animName, agent)
    {
    }

    public override void ChangeState()
    {
        _enemy.targetTrm = _playerCollider.transform;

        _stateMachine.ChangeState(EnemyStateEnum.Appear);
    }
}
