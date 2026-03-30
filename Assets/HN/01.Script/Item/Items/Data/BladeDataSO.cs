using UnityEngine;

[CreateAssetMenu(fileName = "ItemDataSO", menuName = "SO/Item/BladeData")]
public class BladeDataSO : ItemDataSO
{
    public float force;
    public int damage;
    public float speed;
    public float knockbackPower;
}
