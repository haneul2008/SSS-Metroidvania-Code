using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Effect : MonoBehaviour, IPoolable
{
    [SerializeField] private string _poolName;

    public string PoolName => _poolName;

    public GameObject ObjectPrefab => gameObject;

    protected List<ParticleSystem> _particleSystems = new List<ParticleSystem>();
    protected Coroutine _pushCoroutine;
    protected WaitForSeconds _ws;

    protected virtual void Awake()
    {
        _particleSystems = GetComponentsInChildren<ParticleSystem>().ToList();

        float maxDuration = 0;

        foreach (ParticleSystem particle in _particleSystems)
        {
            if (maxDuration < particle.main.duration)
                maxDuration = particle.main.duration;
        }

        _ws = new WaitForSeconds(maxDuration + 0.5f);
    }

    public virtual void PlayEffect(Vector2 pos, bool goToPool = true)
    {
        transform.position = pos;

        foreach (ParticleSystem particle in _particleSystems)
        {
            particle.Play();
        }

        if (goToPool) _pushCoroutine = StartCoroutine(PushCoroutine(_ws));
    }

    public virtual void StopEffect()
    {
        foreach (ParticleSystem particle in _particleSystems)
        {
            particle.Stop();
        }
    }

    protected virtual IEnumerator PushCoroutine(WaitForSeconds duration)
    {
        yield return duration;

        PoolManager.Instance.Push(this);
    }

    protected virtual void OnDisable()
    {
        if (_pushCoroutine != null)
            StopCoroutine(_pushCoroutine);
    }

    public virtual void ResetItem()
    {

    }
}
