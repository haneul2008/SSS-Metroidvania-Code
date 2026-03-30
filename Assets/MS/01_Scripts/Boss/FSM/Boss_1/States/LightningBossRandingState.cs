using System;
using System.Collections;
using UnityEngine;

public class LightningBossRandingState : BossState
{
    private Rigidbody2D _rbCompo;
    private LightningBoss _lightningBoss;
    public LightningBossRandingState(StateMachine<BossEnum, BossState> stateMachine, string animName, Agent agent) : base(stateMachine, animName, agent)
    {
        _rbCompo = m_Boss.GetComponent<Rigidbody2D>();
        _lightningBoss = agent.GetComponent<LightningBoss>();
    }

    public override void Enter()
    {
        base.Enter();
        m_Boss.GetCompo<CrumblingDamageCaster>().TakeDamage(1);
        m_Boss.m_DamageCaster.CastDamage(60,5,true);
        _rbCompo.gravityScale = 1;
        _lightningBoss.OnDownAttackEvent?.Invoke();
        GameManager.Instance.StartCoroutine(ChangeIdleState());
    }

    private IEnumerator ChangeIdleState()
    {
        yield return new WaitForSeconds(0.15f);
        _stateMachine.ChangeState(BossEnum.Idle);
    }

    public override void Update()
    {
        base.Update();
    }

    public override void Exit()
    {
        base.Exit();
    }
}
