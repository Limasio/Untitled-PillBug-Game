using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuGO;
    public bool gameIsPaused = false;
    public AudioMixer audioMixer;

    

    string volumeMain = "Volume";

    private void Awake()
    {
        pauseMenuGO.transform.GetChild(1).transform.GetChild(2).transform.GetChild(1).GetComponent<Slider>().value = PlayerPrefs.GetFloat(volumeMain);
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

    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("Volume", volume);
        PlayerPrefs.SetFloat(volumeMain, volume);
        PlayerPrefs.Save();
    }
}
