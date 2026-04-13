using System;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    private int score;

    public int Score => score;

    public event Action<int> OnScoreChanged;

    public void AddScore(int amount)
    {
        if (amount <= 0) return;

        score += amount;
        OnScoreChanged?.Invoke(score);
    }

    public bool SpendScore(int amount)
    {
        if (amount <= 0) return false;
        if (score < amount) return false;

        score -= amount;
        OnScoreChanged?.Invoke(score);
        return true;
    }

    public void ResetScore()
    {
        score = 0;
        OnScoreChanged?.Invoke(score);
    }
}