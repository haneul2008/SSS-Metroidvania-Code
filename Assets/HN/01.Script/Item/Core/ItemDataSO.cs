using System;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemDataSO", menuName = "SO/Item/ItemDataSO")]
public class ItemDataSO : ScriptableObject
{
    public string itemKey;
    public string className;
    public Sprite itemSprite;
    public string itemDesc;

    [ContextMenu("SetRandomKey")]
    public void SetRandomKey()
    {
        itemKey = Guid.NewGuid().ToString();
    }
}
