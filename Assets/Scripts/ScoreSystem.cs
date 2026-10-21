using System;
using UnityEngine;

public class ScoreSystem : MonoBehaviour
{
    [SerializeField] private int score;

    public event Action<int> ScoreChanged;

    public int CurrentScore => score;

    public void AddPoints(int amount)
    {
        if (amount <= 0)
        {
            Debug.LogError("A pontuação a adicionar tem de ser positiva.", this);
            return;
        }

        score += amount;                 // 1) atualiza o estado
        ScoreChanged?.Invoke(score);     // 2) só depois notifica
    }
}