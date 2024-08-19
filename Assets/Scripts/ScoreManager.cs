using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] TMP_Text scoreText;
    public bool IsGameInPlay;

    private float currentScoreFloat;
    public int currentScore;
    
    // Start is called before the first frame update
    void Awake()
    {
        currentScoreFloat = 0;
        IsGameInPlay = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (IsGameInPlay)
        {
            currentScoreFloat += Time.deltaTime;
            currentScore = (int)currentScoreFloat;
            Debug.Log("Adding Score");
        }

        //scoreText.text = string.Format("Score: ", currentScore);
        scoreText.text = string.Format(currentScore.ToString());
    }

    public void AddScore(int score)
    {
        currentScore = currentScore + score;
    }
}
