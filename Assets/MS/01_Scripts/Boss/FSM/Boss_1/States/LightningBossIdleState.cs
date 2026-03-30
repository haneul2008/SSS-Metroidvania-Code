using System;
using UnityEngine;

public class LightningBossIdleState : BossState
{
    protected Collider2D m_PlayerCollider;

    public LightningBossIdleState(StateMachine<BossEnum, BossState> stateMachine, string animName, Agent agent) : base(stateMachine, animName, agent)
    {
    }

    public override void Enter()
    {
        base.Enter();
        m_BossMovement.StopX();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        m_PlayerCollider = m_Boss.GetPlayerInRange(m_Boss.BossDataSO.detecteRadius);

        if (m_PlayerCollider != null)
        {
            ChangeChaseState();
        }

    }

    private void ChangeChaseState()
    {
        m_Boss.targetTrm = m_PlayerCollider.transform;

        _stateMachine.ChangeState(BossEnum.Chase);
    }
}
