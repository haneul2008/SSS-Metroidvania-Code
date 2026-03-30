using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "SO/Boss/BossListSO")]
public class BossPrefabListSO : ScriptableObject
{
    public List<Boss> bossPrefabList = new List<Boss>();
}
