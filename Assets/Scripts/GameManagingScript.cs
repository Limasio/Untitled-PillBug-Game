using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class GameManagingScript : MonoBehaviour
{
    public GameObject pauseScreen;
    [SerializeField] private PauseMenu pauseMenu;
    [SerializeField] ScoreManager scoreManager;

    public bool gameIsPaused;

    public AudioMixer SFXAudioMixer;
    public AudioMixer MusicAudioMixer;

    private void Awake()
    {
        //pauseMenu = pauseScreen.GetComponent<PauseMenu>();
        Time.timeScale = 1f;
        Time.fixedDeltaTime = 0.02f;
        SceneManager.UnloadSceneAsync("LoadingScene");
    }

    // Start is called before the first frame update
    void Start()
    {
        SFXAudioMixer.SetFloat("VolumeSFX", PlayerPrefs.GetFloat("VolumeSFX"));
        MusicAudioMixer.SetFloat("VolumeMusic", PlayerPrefs.GetFloat("VolumeMusic"));
    }

    // Update is called once per frame
    void Update()
    {
        if (pauseMenu.gameIsPaused)
        {
            gameIsPaused = true;
        }
        else
        {
            gameIsPaused = false;
        }
    }

    public void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void QuitGame()
    {
        scoreManager.ScoreCheck();
        Time.timeScale = 1f;
        pauseScreen.SetActive(false);
        SceneManager.LoadScene("MainMenu");
    }

}
