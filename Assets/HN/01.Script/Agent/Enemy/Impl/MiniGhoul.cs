using UnityEngine;

public sealed class MiniGhoul : Ghoul, IPoolable
{
    [SerializeField] private string _poolName;

    public string PoolName => _poolName;

    public GameObject ObjectPrefab => gameObject;

    public void SpawnMiniGhoul()
    {
        
    }

    public void ResetItem()
    {

    }
}
