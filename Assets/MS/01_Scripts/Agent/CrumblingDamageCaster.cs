using UnityEngine;

public class CrumblingDamageCaster : MonoBehaviour, IAgentComponent
{
    private Agent _agent;

    [SerializeField] private float radius;

    public void Initialize(Agent agent) => _agent = agent;

    public void TakeDamage(int count)
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position,radius);

        foreach (Collider2D collider in colliders)
        {
            if (collider.TryGetComponent<ICrumblingWall>(out ICrumblingWall crumblingWall))
            {
                crumblingWall.Crumbling(count);
            }
        }
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position,radius);
    }
}
