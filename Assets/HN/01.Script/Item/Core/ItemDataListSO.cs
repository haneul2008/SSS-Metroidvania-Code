using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemDataListSO", menuName = "SO/Item/ItemDataListSO")]
public class ItemDataListSO : ScriptableObject
{
    public List<ItemDataSO> list;
}
