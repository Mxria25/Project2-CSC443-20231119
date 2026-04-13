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

        shopPanel.SetActive(false);
    }

    private void Update()
    {
        if (waveManager == null) return;

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

    private void OpenShop()
    {
        shopActive = true;

        shopPanel.SetActive(true);

        // unlock mouse
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        // disable shooting + switching
        if (activeWeapon != null) activeWeapon.enabled = false;
        if (weaponSwitcher != null) weaponSwitcher.enabled = false;
    }

    private void CloseShop()
    {
        shopActive = false;

        shopPanel.SetActive(false);

        // lock mouse back
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // re-enable shooting + switching
        if (activeWeapon != null) activeWeapon.enabled = true;
        if (weaponSwitcher != null) weaponSwitcher.enabled = true;
    }

    public void BuyHeal()
    {
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
        if (!scoreManager.SpendScore(ammoCost))
        {
            ShowInfo("Not enough points!");
            return;
        }

        if (activeWeapon.CurrentWeapon != null)
        {
            activeWeapon.CurrentWeapon.RefillAmmo();
        }

        ShowInfo("Ammo refilled!");
    }

    public void BuyDamage()
    {
        if (!scoreManager.SpendScore(damageCost))
        {
            ShowInfo("Not enough points!");
            return;
        }

        if (activeWeapon.CurrentWeapon != null)
        {
            activeWeapon.CurrentWeapon.Data.damage += 2;
        }

        ShowInfo("Damage increased!");
    }

    private void ShowInfo(string message)
    {
        if (infoText != null)
        {
            infoText.text = message;
        }
    }
}