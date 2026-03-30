using UnityEngine;

public class BossState : State<BossEnum, BossState>    
{
    protected Boss m_Boss;
    protected bool m_EndTriggerCalled;
    protected BossMovement m_BossMovement;
    public BossState(StateMachine<BossEnum, BossState> stateMachine, string animName, Agent agent) : base(stateMachine, animName, agent)
    {
        m_Boss = agent as Boss;
        m_BossMovement = m_Boss.GetCompo<BossMovement>();
    }

    public void AnimationEndTrigger() => m_EndTriggerCalled = true;

    public override void Enter()
    {
        base.Enter();

        m_EndTriggerCalled = false;
    }
}
