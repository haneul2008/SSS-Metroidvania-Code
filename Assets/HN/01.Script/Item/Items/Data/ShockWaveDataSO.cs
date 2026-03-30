using UnityEngine;

[CreateAssetMenu(fileName = "ItemDataSO", menuName = "SO/Item/ShockwaveData")]
public class ShockWaveDataSO : ItemDataSO
{
    public Vector2 offset;
    public float limitY;
    public float force;
    public int damage;
    public float knockbackPower;
    public float distotionDuration;
    public Vector3 distotionEndSize;

    [Header("RaySetting")]
    public float rayDistance;
    public LayerMask whatIsGround;
}
