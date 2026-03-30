using UnityEngine;

public class BoxDamageCaster : MonoBehaviour
{
    [SerializeField] private Vector2 _size;
    [SerializeField] private ContactFilter2D _filter;

    private Collider2D[] _colliders = new Collider2D[1];
    private Agent _agent;
    private AgentMovement _agentMovement;

    public void Initialize(Agent agent)
    {
        _agent = agent;
        _agentMovement = _agent.GetCompo<AgentMovement>();
    }

    public bool CastDamage(int damage, float knockbackPower, bool useNormalVector)
    {
        int cnt = Physics2D.OverlapBox(transform.position, _size,0, _filter, _colliders);

        for (int i = 0; i < cnt; i++)
        {
            if (_colliders[i].TryGetComponent(out Health health))
            {


                health.CastDamage(damage, knockbackPower);


            }
        }

        return cnt > 0;
    }

    private void Knockback(Collider2D colldier, bool useNormalVector, float knockbackPower)
    {
        Vector2 direction = (GetComponent<Collider>().transform.position - transform.position).normalized;
        bool canKnockback = knockbackPower > 0;

        if (canKnockback && useNormalVector)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, direction.magnitude,
            _filter.layerMask);

            direction = hit.normal * -1;
        }

        if (canKnockback)
        {
            _agentMovement.GetKnockback(direction, knockbackPower);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, _size);
    }
}
