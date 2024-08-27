using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuGO;
    public bool gameIsPaused = false;
    public AudioMixer audioMixerM;
    public AudioMixer audioMixerSFX;



    string volumeMusic = "VolumeMusic";
    string volumeSFX = "VolumeSFX";

    private void Awake()
    {
        pauseMenuGO.transform.GetChild(1).transform.GetChild(2).transform.GetChild(1).GetComponent<Slider>().value = PlayerPrefs.GetFloat(volumeMusic);
        pauseMenuGO.transform.GetChild(1).transform.GetChild(3).transform.GetChild(1).GetComponent<Slider>().value = PlayerPrefs.GetFloat(volumeSFX);
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

    public void SetMusicVolume(float volume)
    {
        audioMixerM.SetFloat("VolumeMusic", volume);
        PlayerPrefs.SetFloat(volumeMusic, volume);
        PlayerPrefs.Save();
    }

    public void SetSFXVolume(float volume)
    {
        audioMixerSFX.SetFloat("VolumeSFX", volume);
        PlayerPrefs.SetFloat(volumeSFX, volume);
        PlayerPrefs.Save();
    }
}
