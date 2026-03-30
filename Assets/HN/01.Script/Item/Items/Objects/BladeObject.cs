using DG.Tweening;
using System.Collections;
using UnityEngine;

public class BladeObject : ItemObject, IPoolable
{
    [SerializeField] private string _poolName;
    [SerializeField] private float _lifeTime;
    [SerializeField] private float _fadeDuration;

    public string PoolName => _poolName;

    public GameObject ObjectPrefab => gameObject;

    #region Components
    private Rigidbody2D _rigid;
    private DamageCaster _damageCaster;
    private Collider2D _collider;
    private SpriteRenderer _spriteRenderer;
    #endregion

    private float _moveSpeed;
    private int _damage;
    private float _knockbackPower;
    private Vector2 _dir;
    private Coroutine _pushCoroutine;
    private Tween _fadeTween;
    private WaitForSeconds _ws;
    private bool _isInitialize;

    private void Awake()
    {
        _rigid = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _collider = GetComponent<Collider2D>();
        _damageCaster = GetComponentInChildren<DamageCaster>();

        _ws = new WaitForSeconds(_lifeTime);
    }

    public void SetUp(Item item, Vector2 pos, Vector2 dir, int damage, float speed, float knockbackPower)
    {
        base.SetUp(item);

        if (!_isInitialize)
        {
            _damageCaster.Initialize(item.Player);
            _isInitialize = true;
        }

        transform.position = pos;
        _dir = dir;
        _damage = damage;
        _knockbackPower = knockbackPower;
        _moveSpeed = speed;

        float z = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.eulerAngles = new Vector3(0, 0, z);

        _pushCoroutine = StartCoroutine(PushCoroutine());
    }

    private IEnumerator PushCoroutine()
    {
        yield return _ws;

        _collider.enabled = false;

        _fadeTween = _spriteRenderer.DOFade(0, _fadeDuration).
            OnComplete(() => PoolManager.Instance.Push(this));
    }

    private void Update()
    {
        _rigid.linearVelocity = _dir * _moveSpeed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        _damageCaster.CastDamage(_damage, _knockbackPower, true);
    }

    private void OnDisable()
    {
        if (_fadeTween != null)
            _fadeTween.Kill();

        if (_pushCoroutine != null)
            StopCoroutine(_pushCoroutine);
    }

    public void ResetItem()
    {
        _collider.enabled = true;

        _spriteRenderer.color = Color.white;
    }
}
