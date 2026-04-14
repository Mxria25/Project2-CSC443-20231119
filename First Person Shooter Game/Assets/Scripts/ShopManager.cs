using UnityEngine;
using TMPro;

public class ShopManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private ScoreManager scoreManager;
    [SerializeField] private PlayerHealth playerHealth;
    [SerializeField] private ActiveWeapon activeWeapon;
    [SerializeField] private WeaponSwitcher weaponSwitcher;
    [SerializeField] private WaveManager waveManager;

    [Header("UI")]
    [SerializeField] private GameObject shopPanel;
    [SerializeField] private TextMeshProUGUI infoText;

    [Header("Costs")]
    [SerializeField] private int healCost = 20;
    [SerializeField] private int ammoCost = 15;
    [SerializeField] private int damageCost = 30;

    private bool shopActive = false;

    private void Start()
    {
        if (scoreManager == null) scoreManager = FindAnyObjectByType<ScoreManager>();
        if (playerHealth == null) playerHealth = FindAnyObjectByType<PlayerHealth>();
        if (activeWeapon == null) activeWeapon = FindAnyObjectByType<ActiveWeapon>();
        if (weaponSwitcher == null) weaponSwitcher = FindAnyObjectByType<WeaponSwitcher>();
        if (waveManager == null) waveManager = FindAnyObjectByType<WaveManager>();

        if (shopPanel != null)
        {
            shopPanel.SetActive(false);
        }

        if (playerHealth != null)
        {
            playerHealth.OnDied += HandlePlayerDied;
        }
    }

    private void OnDestroy()
    {
        if (playerHealth != null)
        {
            playerHealth.OnDied -= HandlePlayerDied;
        }
    }

    private void Update()
    {
        if (waveManager == null || playerHealth == null) return;

        if (playerHealth.IsDead)
        {
            if (shopActive)
            {
                CloseShopAfterDeath();
            }
            return;
        }

        bool shouldShowShop =
            !waveManager.WaveInProgress &&
            !waveManager.AllWavesCompleted &&
            waveManager.CurrentWave > 0;

        if (shouldShowShop && !shopActive)
        {
            OpenShop();
        }
        else if (!shouldShowShop && shopActive)
        {
            CloseShop();
        }
    }

    private void HandlePlayerDied()
    {
        CloseShopAfterDeath();
    }

    private void OpenShop()
    {
        shopActive = true;

        if (shopPanel != null)
        {
            shopPanel.SetActive(true);
        }

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        if (activeWeapon != null) activeWeapon.enabled = false;
        if (weaponSwitcher != null) weaponSwitcher.enabled = false;
    }

    private void CloseShop()
    {
        shopActive = false;

        if (shopPanel != null)
        {
            shopPanel.SetActive(false);
        }

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        if (activeWeapon != null) activeWeapon.enabled = true;
        if (weaponSwitcher != null) weaponSwitcher.enabled = true;
    }

    private void CloseShopAfterDeath()
    {
        shopActive = false;

        if (shopPanel != null)
        {
            shopPanel.SetActive(false);
        }

        if (infoText != null)
        {
            infoText.text = "";
        }
    }

    public void BuyHeal()
    {
        if (!CanBuy()) return;

        if (!scoreManager.SpendScore(healCost))
        {
            ShowInfo("Not enough points!");
            return;
        }

        playerHealth.Heal(5);
        ShowInfo("Healed!");
    }

    public void BuyAmmo()
    {
        if (!CanBuy()) return;

        if (!scoreManager.SpendScore(ammoCost))
        {
            ShowInfo("Not enough points!");
            return;
        }

        if (activeWeapon != null && activeWeapon.CurrentWeapon != null)
        {
            activeWeapon.CurrentWeapon.RefillAmmo();
        }

        ShowInfo("Ammo refilled!");
    }

    public void BuyDamage()
    {
        if (!CanBuy()) return;

        if (!scoreManager.SpendScore(damageCost))
        {
            ShowInfo("Not enough points!");
            return;
        }

        if (activeWeapon != null && activeWeapon.CurrentWeapon != null)
        {
            activeWeapon.CurrentWeapon.Data.damage += 2;
        }

        ShowInfo("Damage increased!");
    }

    private bool CanBuy()
    {
        if (!shopActive) return false;
        if (playerHealth == null || playerHealth.IsDead) return false;
        if (scoreManager == null) return false;

        return true;
    }

    private void ShowInfo(string message)
    {
        if (infoText != null)
        {
            infoText.text = message;
        }
    }
}