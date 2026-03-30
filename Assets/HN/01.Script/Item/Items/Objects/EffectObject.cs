using System;
using System.Collections;
using UnityEngine;

public class EffectObject : ItemObject, IPoolable
{
    [SerializeField] private string _poolName;

    public string PoolName => _poolName;

    public GameObject ObjectPrefab => gameObject;

    protected ParticleSystem _particleSystem;
    protected WaitForSeconds _ws;
    protected Coroutine _pushCoroutine;
    protected int _damage;

    protected virtual void Awake()
    {
        _particleSystem = GetComponent<ParticleSystem>();
        _ws = new WaitForSeconds(_particleSystem.main.duration + 0.5f);
    }

    public virtual void SetUp(Item item, Vector2 pos, int damage)
    {
        base.SetUp(item);

        transform.position = pos;

        _damage = damage;

        _particleSystem.Play();

        _pushCoroutine = StartCoroutine(PushCoroutine());
    }

    private IEnumerator PushCoroutine()
    {
        yield return _ws;

        PoolManager.Instance.Push(this);
    }

    public virtual void ResetItem()
    {

    }
}
