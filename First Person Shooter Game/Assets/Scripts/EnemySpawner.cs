using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Pool Settings")]
    [SerializeField] private EnemyHealth enemyPrefab;
    [SerializeField] private int prewarmCount = 10;

    [Header("Spawn Settings")]
    [SerializeField] private Transform[] spawnPoints;

    private ObjectPool<EnemyHealth> pool;

    private void Awake()
    {
        pool = new ObjectPool<EnemyHealth>(enemyPrefab, transform, prewarmCount);
    }

    public EnemyHealth SpawnEnemy()
    {
        if (spawnPoints == null || spawnPoints.Length == 0)
        {
            Debug.LogWarning("No spawn points assigned to EnemySpawner.");
            return null;
        }

        Transform point = spawnPoints[Random.Range(0, spawnPoints.Length)];
        EnemyHealth enemy = pool.Get(point.position, point.rotation);
        return enemy;
    }

    public void DespawnEnemy(EnemyHealth enemy)
    {
        if (enemy == null) return;
        pool.Return(enemy);
    }

    public void DespawnAll()
    {
        pool.ReturnAll();
    }
}