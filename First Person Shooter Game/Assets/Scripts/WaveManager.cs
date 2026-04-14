using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{

    public bool GameWon => currentWave >= maxWaves && aliveEnemies == 0;
    
    [Header("Spawners")]
    [SerializeField] private EnemySpawner runnerSpawner;
    [SerializeField] private EnemySpawner tankSpawner;
    [SerializeField] private EnemySpawner turretSpawner;

    [Header("References")]
    [SerializeField] private PlayerHealth playerHealth;
    [SerializeField] private ScoreManager scoreManager;

    [Header("Timing")]
    [SerializeField] private float startDelay = 2f;
    [SerializeField] private float timeBetweenWaves = 5f;
    [SerializeField] private float spawnDelay = 0.5f;

    [Header("Wave Settings")]
    [SerializeField] private int maxWaves = 5;

    private int currentWave = 0;
    private int aliveEnemies = 0;
    private bool waveInProgress = false;

    private Dictionary<EnemyHealth, EnemySpawner> enemyToSpawner = new Dictionary<EnemyHealth, EnemySpawner>();

    public int CurrentWave => currentWave;
    public bool WaveInProgress => waveInProgress;
    public bool AllWavesCompleted => currentWave >= maxWaves && aliveEnemies == 0 && !waveInProgress;

    private void Start()
    {
        if (playerHealth == null) playerHealth = FindAnyObjectByType<PlayerHealth>();
        if (scoreManager == null) scoreManager = FindAnyObjectByType<ScoreManager>();

        StartCoroutine(WaveLoop());
    }

    private IEnumerator WaveLoop()
    {
        yield return new WaitForSeconds(startDelay);

        while (playerHealth != null && !playerHealth.IsDead && currentWave < maxWaves)
        {
            yield return StartCoroutine(StartNextWave());

            while (aliveEnemies > 0 && !playerHealth.IsDead)
            {
                yield return null;
            }

            waveInProgress = false;

            if (!playerHealth.IsDead && currentWave < maxWaves)
            {
                yield return new WaitForSeconds(timeBetweenWaves);
            }
        }
    }

    private IEnumerator StartNextWave()
    {
        currentWave++;
        waveInProgress = true;

        Debug.Log("Starting Wave " + currentWave);

        int runners = 2 + currentWave * 2;
        int tanks = currentWave >= 2 ? currentWave : 0;
        int turrets = currentWave >= 3 ? 1 : 0;

        // runners
        for (int i = 0; i < runners; i++)
        {
            Spawn(runnerSpawner);
            yield return new WaitForSeconds(spawnDelay);
        }

        // tanks
        for (int i = 0; i < tanks; i++)
        {
            Spawn(tankSpawner);
            yield return new WaitForSeconds(spawnDelay);
        }

        // turrets
        for (int i = 0; i < turrets; i++)
        {
            Spawn(turretSpawner);
            yield return new WaitForSeconds(spawnDelay);
        }
    }

    private void Spawn(EnemySpawner spawner)
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

        scoreManager?.AddScore(10);

        if (enemyToSpawner.TryGetValue(enemy, out EnemySpawner spawner))
        {
            spawner.DespawnEnemy(enemy);
            enemyToSpawner.Remove(enemy);
        }
    }
}