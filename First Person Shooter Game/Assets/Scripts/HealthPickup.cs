using UnityEngine;

public class HealthPickup : Pickup
{
    [SerializeField] private int healAmount = 3;

    protected override bool OnPickedUp(Collider player)
    {
        PlayerHealth playerHealth = player.GetComponentInParent<PlayerHealth>();
        if (playerHealth == null) return false;

        if (playerHealth.CurrentHealth >= playerHealth.MaxHealth) return false;

        playerHealth.Heal(healAmount);
        return true;
    }
}