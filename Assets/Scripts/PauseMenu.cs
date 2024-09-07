using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    

    public GameObject pauseMenuGO;
    public bool gameIsPaused = false;
    //public AudioMixer audioMixerM;
    //public AudioMixer audioMixerSFX;



    public static PauseMenu instance { get; private set; }

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Found more than one Pause Menu in the scene.");
        }
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        GameObject newGO = new GameObject();
        gameObject.transform.parent = newGO.transform; // NO longer DontDestroyOnLoad();
        pauseMenuGO.SetActive(false);

        
    }

    // Update is called once per frame
    void Update()
    {
        if (TimerManager.instance.hasGameEnded == true)
        {
            return;
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gameIsPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }

        
    }

    

    public void PauseGame()
    {
        pauseMenuGO.SetActive(true);
        Time.timeScale = 0f;
        gameIsPaused = true;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        gameIsPaused = false;
        pauseMenuGO.SetActive(false);
    }

    
}
