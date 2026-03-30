using UnityEngine;

public class DamageCaster : MonoBehaviour, IAgentComponent
{
    [SerializeField] private float _radius;
    [SerializeField] private ContactFilter2D _filter;
    [SerializeField] private int _detectCount;

    private Collider2D[] _colliders;
    private Agent _agent;

    public void Initialize(Agent agent)
    {
        _agent = agent;

        _colliders = new Collider2D[_detectCount];
    }

    public bool CastDamage(int damage, float knockbackPower, bool useNormalVector)
    {
        int cnt = Physics2D.OverlapCircle(transform.position, _radius, _filter, _colliders);

        for (int i = 0; i < cnt; i++)
        {
            if (_colliders[i].TryGetComponent(out Health health))
            {
                if (health.CastDamage(damage, knockbackPower))
                {
                        Knockback(health.Agent, knockbackPower, useNormalVector);
                }
            }
        }

        return cnt > 0;
    }

    private void Knockback(Agent agent, float knockbackPower, bool useNormalVector)
    {
        try
        {
            Vector2 direction = (agent.transform.position - transform.position).normalized;
            bool canKnockback = knockbackPower > 0;

            if (!canKnockback) return;

            if (useNormalVector)
            {
                RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, direction.magnitude,
                _filter.layerMask);

                direction = hit.normal * -1;
            }
            else
            {
                AgentRenderer agentRenderer = _agent.GetCompo<AgentRenderer>();
                int xDir = agentRenderer.ReturnFlip() ? -1 : 1;
                direction = new Vector2(xDir, 0);
            }

            AgentMovement movement = agent.GetCompo<AgentMovement>();
            movement?.GetKnockback(direction, knockbackPower);
        }
        catch
        {

        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _radius);
    }
}
