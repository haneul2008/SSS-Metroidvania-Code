using UnityEngine;

public class EnemyAttackState : EnemyState
{
    protected EnemyMovement _enemyMovement;

    public EnemyAttackState(StateMachine<EnemyStateEnum, EnemyState> stateMachine, string animName, Agent agent) : base(stateMachine, animName, agent)
    {
        _enemyMovement = _enemy.GetCompo<EnemyMovement>();
    }

    public override void Enter()
    {
        base.Enter();

        _enemy.canFlip = false;
        _enemyMovement.StopX();
        _enemy.lastAttackTime = Time.time;
    }

    public override void Update()
    {
        base.Update();

        if (_endTriggerCalled)
        {
            _stateMachine.ChangeState(EnemyStateEnum.Idle);
        }
    }

    public override void Exit()
    {
        base.Exit();

        _enemy.canFlip = true;
    }
}
