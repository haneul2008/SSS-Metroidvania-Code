using UnityEngine;

public class LightningBossNormalAttackState : BossState
{
    public LightningBossNormalAttackState(StateMachine<BossEnum, BossState> stateMachine, string animName, Agent agent) : base(stateMachine, animName, agent)
    {
    }

    public override void Enter()
    {
        base.Enter();
        m_BossMovement.StopX();
        m_Boss.IsCanAttack = false;
        
    }

    public override void Exit()
    {
        base.Exit();
        m_Boss.IsCanAttackSetTrue();
    }

    public override void Update()
    {
        base.Update();

        ChangeIdleState();
    }

    private void ChangeIdleState()
    {
        if (m_EndTriggerCalled)
        {
            m_Boss.GetComponent<LightningBoss>().OnAttackEvent?.Invoke();
            _stateMachine.ChangeState(BossEnum.Idle);
        }
    }
}
