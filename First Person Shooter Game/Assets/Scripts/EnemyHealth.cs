using System;
using UnityEngine;

public class EnemyHealth : MonoBehaviour, IPoolable
{
    [SerializeField] private int startingHealth = 3;
    private int currentHealth;

    public int CurrentHealth => currentHealth;
    public int StartingHealth => startingHealth;

    public event Action<EnemyHealth> OnDied;

    private void Awake()
    {
        currentHealth = startingHealth;
    }

    public void TakeDamage(int damage)
    {
        if (damage <= 0) return;

        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        OnDied?.Invoke(this);
    }

    public void OnGetFromPool()
    {
        currentHealth = startingHealth;
    }

    public void OnReturnFromPool()
    {
        OnDied = null;
    }
}