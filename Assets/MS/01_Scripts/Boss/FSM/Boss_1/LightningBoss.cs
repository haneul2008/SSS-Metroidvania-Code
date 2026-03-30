using System;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

public enum BossEnum
{
    Idle,
    Chase,
    NormalAttack,
    Pattern_1,
    Pattern_2,
    Pattern_2_2,
    Pattern_3,
    DownAttack,
    Dead,
    Hit,
    Randing
}

public class LightningBoss : Boss
{
    [HideInInspector] public bool isPattern_3End;

    [Header("LifeCircle Create Position")]
    public Transform maxTransform;
    public Transform minTransform;

    public LifeCircle lifeCirclePrefab;
    public ElectricPole electricPolePrefab;
    public WarningSign warningSign;

    public ParticleSystem deadLeftoverEffect;

    public UnityEvent OnCameraShakingEvent;
    public UnityEvent OnTakeDownEvent;
    public UnityEvent OnDownAttackEvent;
    public UnityEvent OnAttackEvent;
    public UnityEvent OnBoomEvent;

    public GameObject bossSpawner;

    private Player _player;

    protected override void Awake()
    {
        base.Awake();

        m_StateMachine.AddState(BossEnum.Idle, new LightningBossIdleState(m_StateMachine, "Idle", this));
        m_StateMachine.AddState(BossEnum.Chase, new LightningBossChaseState(m_StateMachine, "Chase", this));
        m_StateMachine.AddState(BossEnum.Pattern_1, new LightninigBossPattern_1State(m_StateMachine, "Pattern_1", this));
        m_StateMachine.AddState(BossEnum.Pattern_2, new LightninigBossPattern_2State(m_StateMachine, "Pattern_2", this));
        m_StateMachine.AddState(BossEnum.NormalAttack, new LightningBossNormalAttackState(m_StateMachine, "NormalAttack", this));
        m_StateMachine.AddState(BossEnum.Hit, new LightningBossHitState(m_StateMachine, "Hit", this));
        m_StateMachine.AddState(BossEnum.Pattern_2_2, new LightningBossPattern_2_2State(m_StateMachine, "Pattern_2", this));
        m_StateMachine.AddState(BossEnum.Pattern_3, new LightningBossPattern_3State(m_StateMachine, "Pattern_3", this));
        m_StateMachine.AddState(BossEnum.DownAttack, new LightningBossDownAttackState(m_StateMachine, "DownAttack", this));
        m_StateMachine.AddState(BossEnum.Dead, new LightningBossDeadState(m_StateMachine, "Dead", this));
        m_StateMachine.AddState(BossEnum.Randing, new LightningBossRandingState(m_StateMachine, "Randing", this));

        _player = GameManager.Instance.Player;
        _player.OnDeadEvent += HandlePlayerDead;
    }

    private void HandlePlayerDead(bool isDead)
    {
        Instantiate(bossSpawner);
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        _player.OnDeadEvent -= HandlePlayerDead;
    }

    private void Start()
    {
        m_StateMachine.Initialize(BossEnum.Idle, this);
    }

    public override Enum GetRandomPattern()
    {
        

        if (IsCanPattern == false && TargetDistance() < 3f) return BossEnum.NormalAttack;
        else if(IsCanPattern == false) return BossEnum.Chase;

        float random = UtilityClass.GetRandom_100();


        if (TargetDistance() > 8)
        {
            return BossEnum.Pattern_3;

        }


        if (TargetDistance() > 5)
        {
            if (random > 50)
                return BossEnum.Pattern_2;
            else return BossEnum.Pattern_2_2;
        }



        if (random > 85)
        {
            return BossEnum.Pattern_1;
        }
        else if (random >45)
        {
            return BossEnum.Pattern_2;
        }
        else if (random > 5)
        {
            return BossEnum.Pattern_2_2;
        }
        else 
        {
            return BossEnum.Pattern_3;
        }
    }
}
