using UnityEngine;

[CreateAssetMenu(fileName = "EnemyDataSO", menuName = "SO/EnemyDataSO")]
public class EnemyDataSO : ScriptableObject
{
    [Header("Attack setting")]
    public int attackDamage;
    public float detecteRadius, attackRadius, attackCooldown, detectOffset, knockbackPower;
    public ContactFilter2D contactFilter;
}
