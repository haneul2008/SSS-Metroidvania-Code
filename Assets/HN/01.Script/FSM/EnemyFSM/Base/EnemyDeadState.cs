using UnityEngine;

public class EnemyDeadState : EnemyState
{
    protected EnemyMovement _enemyMovement;

    public EnemyDeadState(StateMachine<EnemyStateEnum, EnemyState> stateMachine, string animName, Agent agent) : base(stateMachine, animName, agent)
    {
        _enemyMovement = _enemy.GetCompo<EnemyMovement>();
    }

    public override void Enter()
    {
        base.Enter();

        _enemyMovement.StopX();
        _enemy.canFlip = false;
        _enemy.SetDead(true);
    }

    public override void Update()
    {
        base.Update();

        if (_endTriggerCalled)
            GameObject.Destroy(_enemy.gameObject);
    }
}
