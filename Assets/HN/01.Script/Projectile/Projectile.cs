using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Projectile : MonoBehaviour, IPoolable
{
    [SerializeField] protected string _poolName;
    [SerializeField] protected float _moveSpeed;

    public string PoolName => _poolName;

    public GameObject ObjectPrefab => gameObject;

    protected Agent _owner;
    protected Vector2 _dir;
    protected Rigidbody2D _rigid;

    protected virtual void Awake()
    {
        _rigid = GetComponent<Rigidbody2D>();
    }

    public virtual void SetUp(Vector2 pos, Agent owner, Vector2 dir)
    {
        transform.position = pos;
        _owner = owner;
        _dir = dir;
    }

    protected virtual void Update()
    {
        _rigid.linearVelocity = new Vector2(_dir.x * _moveSpeed, _rigid.linearVelocityY);
    }

    public void ResetItem()
    {

    }
}
