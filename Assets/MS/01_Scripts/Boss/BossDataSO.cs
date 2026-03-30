using UnityEngine;

[CreateAssetMenu(fileName = "BossDataSO", menuName = "SO/Boss/BossDataSO")]
public class BossDataSO : ScriptableObject
{
    public int bossNumber;

    [Header("Boss Stat")]
    public float bossHP;
    public float bossMoveSpeed;

    [Header("Attack Setting")]
    public int attackDamage;
    public float detecteRadius, attackRadius, attackCooldown, detectOffset, knockbackPower;
    public ContactFilter2D contactFilter;
}
