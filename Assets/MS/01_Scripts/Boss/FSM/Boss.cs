using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;

[Serializable]
public struct LightAndParticle
{
    public Light2D deadLight;
    public ParticleSystem deadParticle;
}

public abstract class Boss : Agent
{
    protected BossStateMachine m_StateMachine;

    [field: SerializeField] public BossDataSO BossDataSO { get; protected set; }

    protected Collider2D[] m_PlayerCollider;
    public DamageCaster m_DamageCaster;
    public BossHealth bossHealth;
    public AgentRenderer agentRenderer;

    [HideInInspector] public Transform targetTrm;
    [HideInInspector] public float lastAttackTime;

    public bool isPatternTrigger;
    public bool IsCanPattern { get; set; } = true;
    public bool IsCanAttack { get; set; } = true;

    [Header("Skill")]
    public KeySkillInfoSO skill;

    [Header("Dead Effect & Light")]
    public LightAndParticle deadLightAndParticle;

    protected override void Awake()
    {
        base.Awake();

        m_StateMachine = new();
        m_PlayerCollider = new Collider2D[1];

        bossHealth = GetCompo<BossHealth>();
        m_DamageCaster = GetCompo<DamageCaster>();
        agentRenderer = GetCompo<AgentRenderer>();

        GetCompo<BossAnimationTrigger>().Initialize(this);
    }

    public void Attack()
    {
        m_DamageCaster.CastDamage(BossDataSO.attackDamage, BossDataSO.knockbackPower, false);
    }

    protected virtual void Update()
    {
        m_StateMachine.CurrentState?.Update();
    }

    protected virtual void FixedUpdate()
    {
        m_StateMachine.CurrentState?.FixedUpdate();
    }

    public void AnimationEndTrigger()
    {
        m_StateMachine.CurrentState.AnimationEndTrigger();
    }

    public virtual void SetDeadState()
    {
        m_StateMachine.ChangeState(BossEnum.Dead);
    }

    public void IsCanPatternSetTrue(float time)
    {
        StartCoroutine(SetTrue(time));
    }

    public IEnumerator SetTrue(float time)
    {
        yield return new WaitForSeconds(time);
        IsCanPattern = true;
    }

    public void IsCanAttackSetTrue()
    {
        StartCoroutine(AttackSetTrue());
    }

    public IEnumerator AttackSetTrue()
    {
        yield return new WaitForSeconds(BossDataSO.attackCooldown);
        IsCanAttack = true;
    }

    public abstract System.Enum GetRandomPattern();

    public void PatternTrigger() => isPatternTrigger = true;

    public Collider2D GetPlayerInRange(float radius)
    {
        int cnt = Physics2D.OverlapCircle(transform.position, radius, BossDataSO.contactFilter, m_PlayerCollider);

        return cnt > 0 ? m_PlayerCollider[0] : null;
    }


    protected void OnDrawGizmosSelected()
    {
        if (BossDataSO == null) return;

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, BossDataSO.detecteRadius);

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, BossDataSO.attackRadius);

        Gizmos.color = Color.white;
    }
    public float TargetDistance()
    {
        if (targetTrm == null) return 0f;

        return Vector3.Distance(targetTrm.position, transform.position);
    }

    
}
