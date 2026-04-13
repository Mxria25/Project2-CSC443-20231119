using TMPro;
using UnityEngine;

public class WaveUI : MonoBehaviour
{
    [SerializeField] private WaveManager waveManager;
    [SerializeField] private TextMeshProUGUI waveText;
    [SerializeField] private TextMeshProUGUI intermissionText;

    private void Start()
    {
        if (waveManager == null)
        {
            waveManager = FindAnyObjectByType<WaveManager>();
        }
    }

    private void Update()
    {
        if (waveManager == null) return;

        if (waveText != null)
        {
            int displayWave = Mathf.Max(1, waveManager.CurrentWave);
            waveText.text = $"Wave: {displayWave}";
        }

        if (intermissionText != null)
        {
            if (!waveManager.WaveInProgress && !waveManager.AllWavesCompleted && waveManager.CurrentWave > 0)
            {
                intermissionText.gameObject.SetActive(true);
                intermissionText.text = "Intermission";
            }
            else if (waveManager.AllWavesCompleted)
            {
                intermissionText.gameObject.SetActive(true);
                intermissionText.text = "All Waves Cleared";
            }
            else
            {
                intermissionText.gameObject.SetActive(false);
            }
        }
    }
}