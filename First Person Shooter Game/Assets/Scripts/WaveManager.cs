using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private EnemySpawner robotSpawner;
    [SerializeField] private PlayerHealth playerHealth;

    [Header("Wave Timing")]
    [SerializeField] private float startDelay = 2f;
    [SerializeField] private float timeBetweenWaves = 5f;
    [SerializeField] private float spawnDelay = 0.5f;

    [Header("Wave Difficulty")]
    [SerializeField] private int startingEnemyCount = 3;
    [SerializeField] private int extraEnemiesPerWave = 2;
    [SerializeField] private int maxWaves = 5;

    [Header("Wave Score")]
    [SerializeField] private ScoreManager scoreManager;
    [SerializeField] private int scorePerEnemy = 10;

    private int currentWave = 0;
    private int aliveEnemies = 0;
    private bool waveInProgress = false;

    private Dictionary<EnemyHealth, EnemySpawner> enemyToSpawner = new Dictionary<EnemyHealth, EnemySpawner>();

    public int CurrentWave => currentWave;
    public int AliveEnemies => aliveEnemies;
    public bool WaveInProgress => waveInProgress;
    public bool AllWavesCompleted => currentWave >= maxWaves && aliveEnemies == 0 && !waveInProgress;

    private void Start()
    {
        if (scoreManager == null)
        {
            scoreManager = FindAnyObjectByType<ScoreManager>();
        }

        if (playerHealth == null)
        {
            playerHealth = FindAnyObjectByType<PlayerHealth>();
        }

        StartCoroutine(WaveLoop());
        
    }

    private IEnumerator WaveLoop()
    {
        yield return new WaitForSeconds(startDelay);

        while (playerHealth != null && !playerHealth.IsDead && currentWave < maxWaves)
        {
            yield return StartCoroutine(StartNextWave());

            while (aliveEnemies > 0 && playerHealth != null && !playerHealth.IsDead)
            {
                yield return null;
            }

            waveInProgress = false;

            if (playerHealth != null && !playerHealth.IsDead && currentWave < maxWaves)
            {
                Debug.Log("Wave cleared. Next wave in " + timeBetweenWaves + " seconds.");
                yield return new WaitForSeconds(timeBetweenWaves);
            }
        }

        if (playerHealth != null && !playerHealth.IsDead && currentWave >= maxWaves && aliveEnemies == 0)
        {
            Debug.Log("All waves completed.");
        }
    }

    private IEnumerator StartNextWave()
    {
        currentWave++;
        waveInProgress = true;

        int enemyCount = startingEnemyCount + (currentWave - 1) * extraEnemiesPerWave;

        Debug.Log("Starting Wave " + currentWave + " with " + enemyCount + " enemies.");

        for (int i = 0; i < enemyCount; i++)
        {
            SpawnFrom(robotSpawner);
            yield return new WaitForSeconds(spawnDelay);
        }
    }

    private void SpawnFrom(EnemySpawner spawner)
    {
        if (spawner == null) return;

        EnemyHealth enemy = spawner.SpawnEnemy();
        if (enemy == null) return;

        aliveEnemies++;
        enemyToSpawner[enemy] = spawner;
        enemy.OnDied += HandleEnemyDied;
    }

    private void HandleEnemyDied(EnemyHealth enemy)
    {
        enemy.OnDied -= HandleEnemyDied;
        aliveEnemies--;

        scoreManager?.AddScore(scorePerEnemy);

        if (enemyToSpawner.TryGetValue(enemy, out EnemySpawner spawner))
        {
            spawner.DespawnEnemy(enemy);
            enemyToSpawner.Remove(enemy);
        }
    }
}