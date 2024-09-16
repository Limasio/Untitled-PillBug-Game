using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] TMP_Text scoreText;

    [SerializeField] TMP_Text gameOverScoreText;
    [SerializeField] TMP_Text gameOverHighScoreText;

    public bool IsGameInPlay;

    private float currentScoreFloat;
    public int currentScore;

    private int highScore;

    public static ScoreManager instance { get; private set; }

    // Start is called before the first frame update
    void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Found more than one Audio Manager in the scene.");
        }
        instance = this;

        currentScoreFloat = 0;
        IsGameInPlay = false;
        highScore = PlayerPrefs.GetInt("highscore", 0);
        Debug.Log("initial high score: " + highScore);
    }

    // Update is called once per frame
    void Update()
    {
        if (IsGameInPlay)
        {
            currentScoreFloat += (Time.deltaTime * 351);
            currentScore = (int)(currentScoreFloat);
            scoreText.text = string.Format("{0:0000000}", currentScore);
            // Debug.Log("Adding Score");
        }

        //scoreText.text = string.Format("Score: ", currentScore);
        
    }

    public void StartScore()
    {
        IsGameInPlay = true;
    }

    public void AddScore(float score)
    {
        currentScoreFloat = currentScoreFloat + score;
        Debug.Log("Score Added: " + score);
        Debug.Log("New Score: " + currentScore);
    }

    public void ScoreCheck()
    {
        IsGameInPlay = false;
        gameOverScoreText.text = ("Score: " + currentScore.ToString());


        if (currentScore > highScore)
        {
            highScore = currentScore;
            PlayerPrefs.SetInt("highscore", currentScore);
            Debug.Log("new high score: " + currentScore);
        }

        gameOverHighScoreText.text = ("High Score: " + highScore.ToString());
    }
}
