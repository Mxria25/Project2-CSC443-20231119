using UnityEngine;
using TMPro;
using StarterAssets;

public class WinUI : MonoBehaviour
{
    [SerializeField] private WaveManager waveManager;
    [SerializeField] private GameObject winPanel;
    [SerializeField] private FirstPersonController playerController;
    [SerializeField] private ActiveWeapon activeWeapon;
    [SerializeField] private WeaponSwitcher weaponSwitcher;

    private bool hasWon = false;

    private void Start()
    {
        if (waveManager == null)
            waveManager = FindAnyObjectByType<WaveManager>();

        if (playerController == null)
            playerController = FindAnyObjectByType<FirstPersonController>();

        if (activeWeapon == null)
            activeWeapon = FindAnyObjectByType<ActiveWeapon>();

        if (weaponSwitcher == null)
            weaponSwitcher = FindAnyObjectByType<WeaponSwitcher>();

        if (winPanel != null)
            winPanel.SetActive(false);
    }

    private void Update()
    {
        if (hasWon) return;

        if (waveManager != null && waveManager.GameWon)
        {
            HandleWin();
        }
    }

    private void HandleWin()
    {
        hasWon = true;

        if (winPanel != null)
            winPanel.SetActive(true);

        if (playerController != null)
            playerController.enabled = false;

        if (activeWeapon != null)
            activeWeapon.enabled = false;

        if (weaponSwitcher != null)
            weaponSwitcher.enabled = false;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}