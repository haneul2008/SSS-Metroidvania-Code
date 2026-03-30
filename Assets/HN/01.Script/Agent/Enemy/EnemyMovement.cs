using UnityEngine;

public class EnemyMovement : AgentMovement, IAgentComponent<Enemy>
{
    [SerializeField] private float _walkAroundSpeed;

    private Enemy _enemy;

    public override void Initialize(Agent agent)
    {
        base.Initialize(agent);

        Initialize(agent as Enemy);
    }

    public void Initialize(Enemy enemy)
    {
        _enemy = enemy;
    }

    public void WalkAround(float dir)
    {
        Rigid.linearVelocityX = dir * _walkAroundSpeed;
    }
}
