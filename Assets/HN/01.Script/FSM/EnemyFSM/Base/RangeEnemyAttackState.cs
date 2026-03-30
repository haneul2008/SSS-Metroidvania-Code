using System;
using UnityEngine;

public abstract class RangeEnemyAttackState : EnemyAttackState
{
    public RangeEnemyAttackState(StateMachine<EnemyStateEnum, EnemyState> stateMachine, string animName, Agent agent) : base(stateMachine, animName, agent)
    {
    }

    public override void Enter()
    {
        base.Enter();

        Vector2 dir = (_enemy.targetTrm.position - _enemy.transform.position).normalized;

        FireProjectile(dir);
    }

    protected abstract void FireProjectile(Vector2 dir);
}
