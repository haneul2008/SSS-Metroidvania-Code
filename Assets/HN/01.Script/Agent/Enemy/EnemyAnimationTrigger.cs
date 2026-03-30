using UnityEngine;

public class EnemyAnimationTrigger : MonoBehaviour, IAgentComponent<Enemy>
{
    private Enemy _enemy;

    public void Initialize(Agent agent) => Initialize(agent as  Enemy);

    public void Initialize(Enemy enemy)
    {
        _enemy = enemy;
    }

    private void AnimationEndTrigger()
    {
        _enemy.AnimationEndTrigger();
    }

    private void AnimationAttackTrigger()
    {
        _enemy.Attack();
    }
}
