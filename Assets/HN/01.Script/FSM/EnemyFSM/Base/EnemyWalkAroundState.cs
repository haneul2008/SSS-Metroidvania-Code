using System;
using UnityEngine;

public class EnemyWalkAroundState : EnemyIdleState
{
    private AgentRenderer _agentRenderer;
    private float _xDir;

    public EnemyWalkAroundState(StateMachine<EnemyStateEnum, EnemyState> stateMachine, string animName, Agent agent) : base(stateMachine, animName, agent)
    {
        _agentRenderer = _enemy.GetCompo<AgentRenderer>();
    }

    public override void Enter()
    {
        base.Enter();

        _enemy.OnReachedWalkPoint += HandleReachedPoint;
    }

    public override void Update()
    {
        base.Update();

        _enemyMovement.WalkAround(_xDir);
    }

    private void HandleReachedPoint(float xDir)
    {
        _enemyMovement.StopX();
        _xDir = xDir;

        _agentRenderer.RightFlip(Mathf.Approximately(xDir, 1f));
    }

    public override void Exit()
    {
        base.Exit();

        _enemy.OnReachedWalkPoint -= HandleReachedPoint;
    }
}
