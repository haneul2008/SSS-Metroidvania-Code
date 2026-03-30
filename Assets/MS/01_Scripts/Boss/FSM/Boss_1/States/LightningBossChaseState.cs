public class LightningBossChaseState : BossState
{
    public LightningBossChaseState(StateMachine<BossEnum, BossState> stateMachine, string animName, Agent agent) : base(stateMachine, animName, agent)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();

        float movex =
            m_Boss.targetTrm.position.x > m_Boss.transform.position.x ? //타겟의 위치가 자신보다 오른쪽인가?
            m_Boss.BossDataSO.bossMoveSpeed : //오른쪽에 존재한다면 오른쪽으로 
            -m_Boss.BossDataSO.bossMoveSpeed;

        m_BossMovement.MoveX(movex); // 왼쪽에 존재한다면 왼쪽으로 이동한다

        m_Boss.agentRenderer.RightFlip(m_Boss.targetTrm.position.x > m_Boss.transform.position.x);
    }

    public override void Update()
    {
        base.Update();

        ChangeAttackState();
    }

    private void ChangeAttackState()
    {
        if (m_Boss.GetPlayerInRange(m_Boss.BossDataSO.attackRadius) != null && m_Boss.IsCanAttack)
            _stateMachine.ChangeState((BossEnum)m_Boss.GetRandomPattern());
    }
}
