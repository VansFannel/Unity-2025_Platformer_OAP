using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class ScoreTracker : MonoBehaviour
{
    public static ScoreTracker instance;

    //[SerializeField] private int scoreStep = 5;
    //[SerializeField] private float scoreIncrementInterval = .2f;
    //[SerializeField] float newHighScoreBannerShowTime = 2f;

    [Header("UI Elements")]
    [SerializeField] private TextMeshProUGUI scoreText;
    //[SerializeField] private TextMeshProUGUI highScoreText;
    //[SerializeField] private TextMeshProUGUI newHighScoreText;

    public int Score { get; private set; }

    //public int HighScore { get; private set; }

    private Coroutine scoreRoutine;
    //private WaitForSeconds scoreIncrementWait;

    //public bool HasNewRecord { get; private set; } = false; 

    private const int cherryScore = 10;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    void Start()
    {
        //scoreIncrementWait = new WaitForSeconds(scoreIncrementInterval);

        ResetScore();
    }

    private void ResetScore()
    {
        Score = 0;

        scoreText.text = "Score: " + Score.ToString("D5");
        //HighScore = PlayerPrefs.GetInt("HighScore", 0);
        //highScoreText.text = "HighScore: " +HighScore.ToString("D5");
        //newHighScoreText.rectTransform.localScale = Vector3.zero;
    }

    /*
    public void StartAddingScore()
    {
        if (scoreRoutine != null)
        {
            return;
        }

        scoreRoutine = StartCoroutine(IncrementScoreRoutine());
    }

    public void StopAddingScore()
    {
        if (scoreRoutine != null)
        {
            StopCoroutine(scoreRoutine);
        }

        if (Score > HighScore)
        {
            HasNewRecord = true;
        }
    }

    public void RegisterNewHighScore()
    {
        HighScore = Score;
        StartCoroutine(NewHighScoreRoutine());

        PlayerPrefs.SetInt("HighScore", HighScore);
        highScoreText.text = "HighScore: " + HighScore.ToString("D5");
    }

    private IEnumerator NewHighScoreRoutine()
    {
        newHighScoreText.text = "NEW Highscore!!\n" + HighScore.ToString("D5");
        newHighScoreText.rectTransform.localScale = Vector3.zero;

        float duration = .55f;
        float elapsed = .0f;
        Vector3 targetScale = Vector3.one;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            newHighScoreText.rectTransform.localScale = Vector3.Lerp(Vector3.zero, targetScale, elapsed / duration);
            yield return null;
        }

        newHighScoreText.rectTransform.localScale = targetScale;

        yield return new WaitForSeconds(newHighScoreBannerShowTime);
        HasNewRecord = false;
    }

    private IEnumerator IncrementScoreRoutine()
    {
        while (true)
        {
            yield return scoreIncrementWait;
            Score += scoreStep;
            scoreText.text = "Score: " + Score.ToString("D5");
        }
    }
    */

    public void AddCherryScore()
    {
        Score += cherryScore;
        scoreText.text = "Score: " + Score.ToString("D5");
    }
}
