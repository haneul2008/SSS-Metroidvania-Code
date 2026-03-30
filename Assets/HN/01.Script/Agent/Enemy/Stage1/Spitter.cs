using UnityEngine;

public class Spitter : Enemy
{
    [SerializeField] private Transform _fireTrm;

    public Transform FireTrm => _fireTrm;

    protected override void Awake()
    {
        base.Awake();

        _stateMachine.AddState(EnemyStateEnum.WalkAround, new EnemyWalkAroundState(_stateMachine, "WalkAround", this));
        _stateMachine.AddState(EnemyStateEnum.Idle, new EnemyIdleState(_stateMachine, "Idle", this));
        _stateMachine.AddState(EnemyStateEnum.Chase, new EnemyChaseState(_stateMachine, "Move", this));
        _stateMachine.AddState(EnemyStateEnum.Attack, new SpitterAttackState(_stateMachine, "Attack", this));
        _stateMachine.AddState(EnemyStateEnum.Dead, new EnemyDeadState(_stateMachine, "Dead", this));

        _stateMachine.Initialize(EnemyStateEnum.WalkAround, this);
    }
}
