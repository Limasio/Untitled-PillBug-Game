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
    
    // Start is called before the first frame update
    void Awake()
    {
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
            currentScoreFloat += Time.deltaTime;
            currentScore = (int)(currentScoreFloat * 200);
            // Debug.Log("Adding Score");
        }

        //scoreText.text = string.Format("Score: ", currentScore);
        scoreText.text = string.Format("{0:0000000}", currentScore);
    }

    public void StartScore()
    {
        IsGameInPlay = true;
    }

    public void AddScore(int score)
    {
        currentScore = currentScore + score;
    }

    public void ScoreCheck()
    {
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
