using UnityEngine;
using UnityEngine.SceneManagement;
using StarterAssets;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] private PlayerHealth playerHealth;
    [SerializeField] private FirstPersonController playerController;
    [SerializeField] private ActiveWeapon activeWeapon;
    [SerializeField] private WeaponSwitcher weaponSwitcher;
    [SerializeField] private TextMeshProUGUI gameOverText;

    private bool gameOver;

    public bool IsGameOver => gameOver;

    private void Start()
    {
        if (playerHealth == null)
        {
            playerHealth = FindAnyObjectByType<PlayerHealth>();
        }

        if (playerController == null)
        {
            playerController = FindAnyObjectByType<FirstPersonController>();
        }

        if (activeWeapon == null)
        {
            activeWeapon = FindAnyObjectByType<ActiveWeapon>();
        }

        if (weaponSwitcher == null)
        {
            weaponSwitcher = FindAnyObjectByType<WeaponSwitcher>();
        }

        if (playerHealth != null)
        {
            playerHealth.OnDied += HandlePlayerDied;
        }

        if (gameOverText != null)
        {
            gameOverText.gameObject.SetActive(false);
        }
    }

    private void OnDestroy()
    {
        if (playerHealth != null)
        {
            playerHealth.OnDied -= HandlePlayerDied;
        }
    }

    private void HandlePlayerDied()
    {
        if (gameOver) return;

        gameOver = true;
        Debug.Log("GAME OVER");

        if (playerController != null)
        {
            playerController.enabled = false;
        }

        if (activeWeapon != null)
        {
            activeWeapon.enabled = false;
        }

        if (weaponSwitcher != null)
        {
            weaponSwitcher.enabled = false;
        }

        if (gameOverText != null)
        {
            gameOverText.gameObject.SetActive(true);
            gameOverText.text = "GAME OVER";
        }

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}