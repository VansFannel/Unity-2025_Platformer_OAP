using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class ScoreTracker : MonoBehaviour
{
    public static ScoreTracker instance;

    [Header("UI Elements")]
    [SerializeField] private TextMeshProUGUI scoreText;

    public int Score { get; private set; }

    private Coroutine scoreRoutine;

    private const int cherryScore = 10;
    private const int bunnyScore = 50;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    void Start()
    {
        ResetScore();
    }

    private void ResetScore()
    {
        Score = 0;

        scoreText.text = "Score: " + Score.ToString("D5");
    }

    public void AddCherryScore()
    {
        Score += cherryScore;
        scoreText.text = "Score: " + Score.ToString("D5");
    }

    internal void AddBunnyScore()
    {
        Score += bunnyScore;
        scoreText.text = "Score: " + Score.ToString("D5");
    }
}
