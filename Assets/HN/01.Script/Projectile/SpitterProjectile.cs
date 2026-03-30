using UnityEngine;

public class SpitterProjectile : Projectile
{
    [SerializeField] private float _fireAngle = 30f;
    [SerializeField] private float _threshold = 0.5f;

    private DamageCaster _damageCaster;

    private int _travelHash = Animator.StringToHash("Travel");
    private int _burstHash = Animator.StringToHash("Burst");

    private Animator _anim;

    private EnemyDataSO _enemyData;
    private Collider2D _colldier;

    protected override void Awake()
    {
        base.Awake();

        _damageCaster = GetComponentInChildren<DamageCaster>();
        _damageCaster.Initialize(null);

        _anim = GetComponent<Animator>();
        _colldier = GetComponent<Collider2D>();
    }

    public override void SetUp(Vector2 pos, Agent owner, Vector2 dir)
    {
        base.SetUp(pos, owner, dir);

        _anim.SetBool(_travelHash, true);

        Spitter spitter = owner as Spitter;
        _enemyData = spitter.EnemyData;

        SetVelocity(dir);

        _colldier.enabled = true;
    }

    public void SetVelocity(Vector2 dir)
    {
        float angle = _fireAngle * Mathf.Deg2Rad;

        float cos = Mathf.Cos(angle);
        float tan = Mathf.Tan(angle);
        float sin = Mathf.Sin(angle);
        float gravity = Physics2D.gravity.magnitude;

        float distance = Mathf.Abs(dir.x);

        if(distance < _threshold)
            distance = _threshold;

        float yOffset = dir.y;

        float vZero = 1 / cos * Mathf.Sqrt(0.5f * gravity * Mathf.Pow(distance, 2)
                                            / (distance * tan + yOffset * tan));
        if (float.IsNaN(vZero))
        {
            vZero = 4f;
        }

        float xDirection = -Mathf.Sign(dir.x);

        Vector2 velocity = new Vector2(xDirection * _moveSpeed * vZero * cos, vZero * sin);

        _rigid.AddForce(velocity, ForceMode2D.Impulse);
    }

    protected override void Update()
    {
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        _damageCaster.CastDamage(_enemyData.attackDamage, _enemyData.knockbackPower, true);

        _anim.SetBool(_travelHash, false);
        _anim.SetTrigger(_burstHash);

        _rigid.linearVelocity = Vector2.zero;

        _colldier.enabled = false;
    }

    public void Push() => PoolManager.Instance.Push(this);
}
