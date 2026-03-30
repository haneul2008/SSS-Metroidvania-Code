using UnityEngine;

public class BossSpawner : MonoBehaviour
{
    [SerializeField] private BossPrefabListSO bossPrefabListSO;
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private int _bossNumber;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            foreach (Boss item in bossPrefabListSO.bossPrefabList)
            {
                if (item.BossDataSO.bossNumber == _bossNumber)
                {
                    Instantiate(item, _spawnPoint.position, Quaternion.identity);
                }
            }
        }

        Destroy(gameObject);

    }
}
