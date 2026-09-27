using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    // "Instance" permite que cualquier script lo encuentre sin buscarlo
    public static ScoreManager Instance;

    public TMP_Text scoreText;
    int score = 0;

    void Awake()
    {
        Instance = this;
    }

    public void AddPoints(int amount)
    {
        score += amount;
        scoreText.text = "Puntos: " + score;
    }
}