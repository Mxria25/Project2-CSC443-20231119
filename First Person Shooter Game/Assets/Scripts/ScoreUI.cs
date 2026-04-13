using TMPro;
using UnityEngine;

public class ScoreUI : MonoBehaviour
{
    [SerializeField] private ScoreManager scoreManager;
    [SerializeField] private TextMeshProUGUI scoreText;

    private void Start()
    {
        if (scoreManager == null)
        {
            scoreManager = FindAnyObjectByType<ScoreManager>();
        }

        if (scoreManager != null)
        {
            scoreManager.OnScoreChanged += UpdateScore;
            UpdateScore(scoreManager.Score);
        }
    }

    private void OnDestroy()
    {
        if (scoreManager != null)
        {
            scoreManager.OnScoreChanged -= UpdateScore;
        }
    }

    private void UpdateScore(int newScore)
    {
        if (scoreText != null)
        {
            scoreText.text = $"Score: {newScore}";
        }
    }
}