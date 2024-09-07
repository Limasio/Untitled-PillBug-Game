using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Security.Cryptography;

public class TimerManager : MonoBehaviour
{
    [SerializeField] TMP_Text timerText;
    [SerializeField] float timeStart;
    [SerializeField] float timeLeft;
    [SerializeField] float timeBonus;
    [SerializeField] ScoreManager scoreManager;

    [SerializeField] GameObject GameOverPanel;

    public bool hasTimerStarted;

    public static TimerManager instance { get; private set; }
    public bool hasGameEnded { get; private set; }

    [Header("Time Bonuses For Distance Traveled")]
    [SerializeField] float timeBonusInitial;
    [SerializeField] float timeBonus45sec;
    [SerializeField] float timeBonus1min30sec;
    [SerializeField] float timeBonus3min;
    [SerializeField] float timeBonus5min;
    [SerializeField] GameObject music;

    private float timer = 0f;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Found more than one Timer Manager in the scene.");
        }
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        timeLeft = timeStart;
        hasTimerStarted = false;
        hasGameEnded = false;
    }

    void FixedUpdate()
    {
        if (hasTimerStarted)
        {
            timer += Time.deltaTime;
            Debug.Log(timer);
        }
    }

    // ik this is bad but idgaf bc i am placing blocks n shit cuz im in fucking minecraftttt i am placing blocks n shit cuz im in fucking minecraftttt ohhhhh my goddddd is that... fuckin pig
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
            if(timer < 45)
            {
                timeLeft += timeBonusInitial;
            }
            else if(timer < 105)
            {
                timeLeft += timeBonus45sec;
            }
            else if(timer < 180)
            {
                timeLeft += timeBonus1min30sec;
            }
            else if(timer < 300)
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
        hasGameEnded = true;
        hasTimerStarted = false;
        scoreManager.ScoreCheck();
        AudioManager.instance.StopMusic();
        AudioManager.instance.PlayOneShot(FMODEvents.instance.gameOverSax, this.transform.position);
        timerText.text = "00:00";
        GameOverPanel.SetActive(true);
        Time.timeScale = 0f;
        // SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        //SceneManager.LoadScene(sceneName:"MainMenu");
    }
}
