using System;
using UnityEngine;

public class EnemyChaseState : EnemyState
{
    protected EnemyMovement _enemyMovement;

    public EnemyChaseState(StateMachine<EnemyStateEnum, EnemyState> stateMachine, string animName, Agent agent) : base(stateMachine, animName, agent)
    {
        _enemyMovement = _enemy.GetCompo<EnemyMovement>();
    }


    public override void Update()
    {
        base.Update();

        ChaseTarget();

        float distance = Vector2.Distance(_enemy.transform.position, _enemy.targetTrm.position);

        CheckChaseToIdle(distance);
        CheckChaseToAttack(distance);
    }

    private void CheckChaseToAttack(float distance)
    {
        bool isPlayerInRange = distance < _enemy.EnemyData.attackRadius;
        bool isOverCooldown = _enemy.lastAttackTime + _enemy.EnemyData.attackCooldown < Time.time;

        if (isPlayerInRange && isOverCooldown && _enemyMovement.IsGround)
        {
            _stateMachine.ChangeState(EnemyStateEnum.Attack);
        }
    }

    private void CheckChaseToIdle(float distance)
    {
        if(distance > _enemy.EnemyData.detecteRadius + _enemy.EnemyData.detectOffset)
        {
            _stateMachine.ChangeState(EnemyStateEnum.Idle);
        }
    }

    private void ChaseTarget()
    {
        if (_enemy.targetTrm == null) return;

        Vector2 dir = (_enemy.targetTrm.position - _enemy.transform.position).normalized;
        _enemyMovement?.MoveX(dir.x);
    }
}
