using UnityEngine;

[CreateAssetMenu(fileName = "ItemDataSO", menuName = "SO/Item/SlashData")]
public class SlashDataSO : ItemDataSO
{
    public int damage;
    public int slashcnt;
    public float slashDelay;
    public float slashCooltime;
    public float radius;
    public ContactFilter2D contactFilter;
    public int detectCnt;
}
