using System;
using UnityEngine;

public class EnemyIdleState : EnemyState
{
    protected Collider2D _playerCollider;
    protected Health _health;
    protected EnemyMovement _enemyMovement;

    public EnemyIdleState(StateMachine<EnemyStateEnum, EnemyState> stateMachine, string animName, Agent agent) : base(stateMachine, animName, agent)
    {
        _health = _enemy.GetCompo<Health>();
        _enemyMovement = _enemy.GetCompo<EnemyMovement>();
    }

    public override void Enter()
    {
        base.Enter();

        _enemyMovement.StopX();
        _health.OnDamagedEvent.AddListener(HandleDamaged);
    }

    private void HandleDamaged()
    {
        if(_enemy.targetTrm != null)
        {
            _playerCollider = _enemy.GetPlayerInRange(2);

            if (_playerCollider == null) return;
        }

        ChangeState();
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();

        _playerCollider = _enemy.GetPlayerInRange();

        if (_playerCollider != null)
        {
            ChangeState();
        }
    }

    public virtual void ChangeState()
    {
        _enemy.targetTrm = _playerCollider.transform;

        _stateMachine.ChangeState(EnemyStateEnum.Chase);
    }

    public override void Exit()
    {
        base.Exit();

        _health.OnDamagedEvent.RemoveListener(HandleDamaged);
        _enemy.canFlip = true;
    }
}
