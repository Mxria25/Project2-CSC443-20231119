using UnityEngine;
using UnityEngine.AI;
using StarterAssets;

public class Robot : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float attackRange = 2f;

    [Header("Combat")]
    [SerializeField] private float attackRate = 1f;
    [SerializeField] private int damage = 1;

    private NavMeshAgent agent;
    private FirstPersonController player;
    private PlayerHealth playerHealth;
    private float nextAttackTime;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        player = FindAnyObjectByType<FirstPersonController>();

        if (player != null)
        {
            playerHealth = player.GetComponent<PlayerHealth>();
        }
    }

    private void OnEnable()
    {
        if (agent != null)
        {
            agent.enabled = false;
            agent.enabled = true;
            agent.Warp(transform.position);
        }

        if (player == null)
        {
            player = FindAnyObjectByType<FirstPersonController>();
        }

        if (player != null && playerHealth == null)
        {
            playerHealth = player.GetComponent<PlayerHealth>();
        }
    }

    private void Update()
    {
        if (player == null || playerHealth == null) return;
        if (!agent.isOnNavMesh) return;
        if (playerHealth.IsDead) return;

        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

        if (distanceToPlayer > attackRange)
        {
            agent.isStopped = false;
            agent.SetDestination(player.transform.position);
        }
        else
        {
            agent.isStopped = true;

            if (Time.time >= nextAttackTime)
            {
                nextAttackTime = Time.time + (1f / attackRate);
                playerHealth.TakeDamage(damage);
            }
        }
    }
}