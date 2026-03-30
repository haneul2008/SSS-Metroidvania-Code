using UnityEngine;

public class ItemObject : MonoBehaviour
{
    protected Item _item;

    public virtual void SetUp(Item item)
    {
        _item = item;
    }
}
