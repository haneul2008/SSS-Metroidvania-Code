using UnityEngine;

public class LightningBossDeadState : BossState
{
    public LightningBossDeadState(StateMachine<BossEnum, BossState> stateMachine, string animName, Agent agent) : base(stateMachine, animName, agent)
    {
    }

    public override void Enter()
    {
        base.Enter();
        m_BossMovement.StopX();

        m_Boss.GetPlayerInRange(30)
            .gameObject.GetComponent<Player>()
            .GetCompo<SkillFactory>()
            .AddSkill(m_Boss.skill.skillName);

        m_Boss.gameObject.layer = LayerMask.NameToLayer("Dead");

        GameObject.Instantiate(m_Boss.deadLightAndParticle.deadLight, m_Boss.transform.position, Quaternion.identity);
        GameObject.Instantiate(m_Boss.deadLightAndParticle.deadParticle, m_Boss.transform.position,Quaternion.identity);
        GameObject.Instantiate(m_Boss.GetComponent<LightningBoss>().deadLeftoverEffect, m_Boss.transform);
    }

    public override void Exit()
    {
        base.Exit();
    }
}
