using UnityEngine;
using DG.Tweening;

public class SpitterAttackState : RangeEnemyAttackState
{
    private Spitter _spitter;

    public SpitterAttackState(StateMachine<EnemyStateEnum, EnemyState> stateMachine, string animName, Agent agent) : base(stateMachine, animName, agent)
    {
        _spitter = _enemy as Spitter;
    }

    protected override void FireProjectile(Vector2 dir)
    {
        Vector2 targetPos = _spitter.targetTrm.position;

        DOVirtual.DelayedCall(0.3f, () =>
        {
            SpitterProjectile projectile = PoolManager.Instance.Pop("SpitterProjectile") as SpitterProjectile;

            Vector2 firePos = _spitter.FireTrm.position;

            dir = (firePos - targetPos).normalized;

            projectile.SetUp(firePos, _spitter, dir);

            _enemy.OnAttackEvent?.Invoke();
        });
    }
}
