using TMPro;
using UnityEngine;

public class PlayerHealthUI : MonoBehaviour
{
    [SerializeField] private PlayerHealth playerHealth;
    [SerializeField] private TextMeshProUGUI healthText;

    private void Start()
    {
        if (playerHealth == null)
        {
            playerHealth = FindAnyObjectByType<PlayerHealth>();
        }

        if (playerHealth != null)
        {
            playerHealth.OnHealthChanged += UpdateHealthUI;
            UpdateHealthUI(playerHealth.CurrentHealth, playerHealth.MaxHealth);
        }
    }

    private void OnDestroy()
    {
        if (playerHealth != null)
        {
            playerHealth.OnHealthChanged -= UpdateHealthUI;
        }
    }

    private void UpdateHealthUI(int current, int max)
    {
        if (healthText != null)
        {
            healthText.text = $"Health: {current} / {max}";
        }
    }
}