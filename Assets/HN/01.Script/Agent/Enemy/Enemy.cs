using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Enemy : Agent
{
    public UnityEvent OnAttackEvent;

    public event Action<float> OnReachedWalkPoint;

    [field: SerializeField] public EnemyDataSO EnemyData { get; protected set; }

    [SerializeField] private List<float> _walkDistances;
    [SerializeField] private string _enemyLayer;
    [SerializeField] private string _deadLayer;

    protected EnemyStateMachine _stateMachine;
    protected Collider2D[] _playerCollider;
    protected DamageCaster _damageCaster;
    protected AgentRenderer _renderer;

    private List<float> _targetX = new List<float>();
    private int _currentWalkIndex;
    private Vector2 _originPos;
    private float _ignoreCheckWalkTerm = 0.1f;
    private float _lastReachTime;

    [HideInInspector] public Transform targetTrm;
    [HideInInspector] public float lastAttackTime;

    public bool canFlip = true;

    protected override void Awake()
    {
        base.Awake();

        _stateMachine = new EnemyStateMachine();
        _playerCollider = new Collider2D[1];

        _damageCaster = GetCompo<DamageCaster>();

        _renderer = GetCompo<AgentRenderer>();

        _originPos = transform.position;

        _walkDistances.ForEach(distacne =>
        {
            _targetX.Add(_originPos.x + distacne);
        });
    }

    private void Start()
    {
        if (_targetX.Count == 0) return;

        float xDir = Mathf.Sign(_targetX[0] - transform.position.x);
        OnReachedWalkPoint?.Invoke(xDir);
    }

    protected virtual void Update()
    {
        _stateMachine.CurrentState?.Update();
        CheckWalkPoint();

        if (canFlip && targetTrm != null)
            _renderer.RightFlip(targetTrm.position.x > transform.position.x);
    }

    protected virtual void FixedUpdate()
    {
        _stateMachine.CurrentState?.FixedUpdate();
    }

    public void AnimationEndTrigger()
    {
        _stateMachine.CurrentState.AnimationEndTrigger();
    }

    public virtual void Attack()
    {
        _damageCaster.CastDamage(EnemyData.attackDamage, EnemyData.knockbackPower, false);
        OnAttackEvent?.Invoke();
    }

    public virtual void SetDeadState()
    {
        _stateMachine.ChangeState(EnemyStateEnum.Dead);
    }

    public Collider2D GetPlayerInRange(float rangeMuliplier = 1f)
    {
        int cnt = Physics2D.OverlapCircle(transform.position, EnemyData.detecteRadius * rangeMuliplier, EnemyData.contactFilter, _playerCollider);

        return cnt > 0 ? _playerCollider[0] : null;
    }

    public override void SetDead(bool dead)
    {
        base.SetDead(dead);

        gameObject.layer = dead? LayerMask.NameToLayer(_deadLayer) : LayerMask.NameToLayer(_enemyLayer);
    }

    private void CheckWalkPoint()
    {
        if (_targetX.Count == 0) return;

        float targetX = _targetX[_currentWalkIndex];

        bool isReachedLeftPos = targetX < _originPos.x && transform.position.x < targetX;
        bool isReachedRightPos = targetX >= _originPos.x && transform.position.x >= targetX;

        bool isIgnoreCheck = _ignoreCheckWalkTerm + _lastReachTime > Time.time;

        if (!isIgnoreCheck && (isReachedLeftPos || isReachedRightPos))
        {
            _currentWalkIndex++;

            if (_currentWalkIndex == _targetX.Count) _currentWalkIndex = 0;

            float xDir = Mathf.Sign(targetX - transform.position.x);

            OnReachedWalkPoint?.Invoke(xDir);

            _lastReachTime = Time.time;
        }
    }

#if UNITY_EDITOR
    protected void OnDrawGizmosSelected()
    {
        if (EnemyData == null) return;

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, EnemyData.detecteRadius);

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, EnemyData.attackRadius);

        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position, EnemyData.detecteRadius + EnemyData.detectOffset);

        Gizmos.color = Color.red;

        Vector2 pos = Vector2.zero;

        if (Application.isPlaying)
            pos = new Vector2(_originPos.x, _originPos.y - transform.localScale.x / 2);
        else
            pos = new Vector2(transform.position.x, transform.position.y - transform.localScale.x / 2);

        foreach (float distance in _walkDistances)
        {
            Gizmos.DrawLine(pos, pos + new Vector2(distance, 0));
        }
    }
#endif
}
