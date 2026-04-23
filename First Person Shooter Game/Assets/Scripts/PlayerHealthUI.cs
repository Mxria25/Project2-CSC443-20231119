using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthUI : MonoBehaviour
{
    [SerializeField] private PlayerHealth playerHealth;
    [SerializeField] private Image healthFillImage;
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
        float fillPercent = (float)current / max;

        if (healthFillImage != null)
        {
            healthFillImage.fillAmount = fillPercent;
        }

        if (healthText != null)
        {
            healthText.text = $"{current} / {max}";
        }
    }
}