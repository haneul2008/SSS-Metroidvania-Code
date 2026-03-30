using System;
using UnityEngine;

public class LifeCircle : MonoBehaviour, IAgentComponent<LightningBoss>
{
    public Action<LifeCircle> OnDeadCircleEvent;

    [SerializeField] private ParticleSystem _destroyParticle;

    public Health health;

    public void CircleDestroy()
    {

        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        OnDeadCircleEvent?.Invoke(this);
        Instantiate(_destroyParticle, transform.position, Quaternion.identity);
        OnDeadCircleEvent = null;
    }

    public void Initialize(LightningBoss agent)
    {

    }

    public void Initialize(Agent agent) => Initialize(agent as LightningBoss);
}
