using System;
using UnityEngine;
using UnityEngine.Events;

public sealed class Player : Agent
{
    public Action<PlayerState> OnPlayerStateChange;
    public event Action<bool> OnAttackEvent;
    public event Action<bool> OnDeadEvent;

    public bool isCanJump;

    [SerializeField] private InputReader _inputReader;
    public PlayerStateMachine StateMachine { get; private set; }
    private DamageCaster _damageCaster;
    private float rollTiemr;
    [SerializeField] private float rollTimerMax = 0.75f;

    [Header("Test")]
    [SerializeField] private int _atk1Damage;
    [SerializeField] private float _knockbackPower;


    public UnityEvent PlayerNowMove;
    protected override void Awake()
    {
        _components.Add(_inputReader.GetType(), _inputReader);

        base.Awake();

        _inputReader.Initialize(this);

        StateMachine = new PlayerStateMachine();
        StateMachine.AddState(PlayerStateEnum.Idle, new PlayerIdleState(StateMachine, "Idle", this));
        StateMachine.AddState(PlayerStateEnum.Move, new PlayerMoveState(StateMachine, "Move", this));
        StateMachine.AddState(PlayerStateEnum.Jump, new PlayerJumpState(StateMachine, "Jump", this));
        StateMachine.AddState(PlayerStateEnum.Fall, new PlayerFallState(StateMachine, "Fall", this));
        StateMachine.AddState(PlayerStateEnum.Attack1, new PlayerAttack1State(StateMachine, "Attack1", this));
        StateMachine.AddState(PlayerStateEnum.Roll, new PlayerRollState(StateMachine, "Roll", this));
        StateMachine.AddState(PlayerStateEnum.Dash, new PlayerDashState(StateMachine, "Dash", this));
        StateMachine.AddState(PlayerStateEnum.Dead, new PlayerDeadState(StateMachine, "Dead", this));

        StateMachine.Initialize(PlayerStateEnum.Idle, this);

        _damageCaster = transform.Find("DamageCaster").GetComponent<DamageCaster>();
    }

    public void SetDeadState() => StateMachine.ChangeState(PlayerStateEnum.Dead);

    public override void SetDead(bool dead)
    {
        base.SetDead(dead);

        OnDeadEvent?.Invoke(dead);
    }

    public void Attack()
    {
        bool isAttackSuccess = _damageCaster.CastDamage(_atk1Damage, _knockbackPower, false);
        OnAttackEvent?.Invoke(isAttackSuccess);
    }

    private void Update()
    {
        StateMachine.CurrentState?.Update();

        rollTiemr += Time.deltaTime;


    }
    public bool RollTimer()
    {
        if (rollTimerMax < rollTiemr)
        {
            rollTiemr = 0;
            return true;
        }
        else
        {
            return false;
        }
    }

    private void FixedUpdate()
    {
        StateMachine.CurrentState?.FixedUpdate();
    }

    public void AnimationEndTrigger()
    {
        StateMachine.CurrentState?.AnimationEndTrigger();
    }

    public void OnPlayerNowMove()
    {
        PlayerNowMove?.Invoke();
    }
}
