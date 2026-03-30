using UnityEngine;

public class LightningBossHitState : BossState
{
    private float HIT_DELAY_TIME = 5f;
    private float startTime;

    private LightningBoss _lightningBoss;
    public LightningBossHitState(StateMachine<BossEnum, BossState> stateMachine, string animName, Agent agent) : base(stateMachine, animName, agent)
    {
    }

    public override void Enter()
    {
        base.Enter();
        _lightningBoss = m_Boss as LightningBoss;
        _lightningBoss.OnCameraShakingEvent?.Invoke();
        Debug.Log(_stateMachine.CurrentState + " 들어감");
        startTime = Time.time;
        m_BossMovement.StopX();
        m_BossMovement.CanMoveSet(false);
    }

    public override void Update()
    {
        base.Update();

        ChangeIdleState();
    }

    private void ChangeIdleState()
    {
        if (startTime + HIT_DELAY_TIME < Time.time)
            _stateMachine.ChangeState(BossEnum.Idle);
    }

    public override void Exit()
    {
        base.Exit();
        Debug.Log(_stateMachine.CurrentState + " 나감");
        m_BossMovement.CanMoveSet(true);
    }
}
