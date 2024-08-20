using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class TimerManager : MonoBehaviour
{
    [SerializeField] TMP_Text timerText;
    [SerializeField] float timeStart;
    [SerializeField] float timeLeft;
    [SerializeField] float timeBonus;
    [SerializeField] ScoreManager scoreManager;

    public bool hasTimerStarted;

    [Header("Time Bonuses For Distance Traveled")]
    [SerializeField] float timeBonusInitial;
    [SerializeField] float timeBonus45sec;
    [SerializeField] float timeBonus1min30sec;
    [SerializeField] float timeBonus3min;
    [SerializeField] float timeBonus5min;
    [SerializeField] GameObject music;

    // Start is called before the first frame update
    void Start()
    {
        timeLeft = timeStart;
        hasTimerStarted = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (hasTimerStarted)
        {
            timeLeft -= Time.deltaTime;
            if (timeLeft > 0)
            {
                int min = Mathf.FloorToInt(timeLeft / 60);
                int sec = Mathf.FloorToInt(timeLeft % 60);
                timerText.text = string.Format("{0:00}:{1:00}", min, sec);
            }
            else
            {
                GameOver();
            }
        }
        
    }

    public void StartTime()
    {
        hasTimerStarted = true;
    }

    public void AddTime()
    {
        if (hasTimerStarted)
        {
            //I should be using a case statment but I'm not sorrrry colbyyyyy
            if(scoreManager.currentScore < 45)
            {
                timeLeft += timeBonusInitial;
            }
            else if(scoreManager.currentScore < 105)
            {
                timeLeft += timeBonus45sec;
            }
            else if(scoreManager.currentScore < 180)
            {
                timeLeft += timeBonus1min30sec;
            }
            else if(scoreManager.currentScore < 300)
            {
                timeLeft += timeBonus3min;
            }
            else
            {
                timeLeft += timeBonus5min;
            }
            //timeLeft += timeBonus;
            int min = Mathf.FloorToInt(timeLeft / 60);
            int sec = Mathf.FloorToInt(timeLeft % 60);
            timerText.text = string.Format("{0:00}:{1:00}", min, sec);
        }
    }

    public void LoseTime(float timeLoss)
    {
        if (hasTimerStarted)
        {
            timeLeft -= timeLoss;
            int min = Mathf.FloorToInt(timeLeft / 60);
            int sec = Mathf.FloorToInt(timeLeft % 60);
            timerText.text = string.Format("{0:00}:{1:00}", min, sec);
        }
    }

    void GameOver()
    {
        hasTimerStarted = false;
        scoreManager.ScoreCheck();
        music.SetActive(false);
        // SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        SceneManager.LoadScene(sceneName:"MainMenu");
    }
}
