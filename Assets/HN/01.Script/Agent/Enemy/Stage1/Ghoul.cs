using System;
using UnityEngine;

public class Ghoul : Enemy
{
    protected override void Awake()
    {
        base.Awake();

        _stateMachine.AddState(EnemyStateEnum.WalkAround, new EnemyWalkAroundState(_stateMachine, "WalkAround", this));
        _stateMachine.AddState(EnemyStateEnum.Idle, new GhoulIdleState(_stateMachine, "Idle", this));
        _stateMachine.AddState(EnemyStateEnum.Appear, new GhoulWakeupState(_stateMachine, "WakeUp", this));
        _stateMachine.AddState(EnemyStateEnum.Chase, new EnemyChaseState(_stateMachine, "Move", this));
        _stateMachine.AddState(EnemyStateEnum.Attack, new EnemyAttackState(_stateMachine, "Attack", this));
        _stateMachine.AddState(EnemyStateEnum.Dead, new EnemyDeadState(_stateMachine, "Dead", this));

        _stateMachine.Initialize(EnemyStateEnum.WalkAround, this);
    }
}
