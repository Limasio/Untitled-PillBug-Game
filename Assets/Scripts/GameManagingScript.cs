using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.InputSystem.UI;
using UnityEngine.SceneManagement;

public class GameManagingScript : MonoBehaviour
{
    public GameObject pauseScreen;
    [SerializeField] private PauseMenu pauseMenu;
    [SerializeField] ScoreManager scoreManager;
    [SerializeField] GameObject virtualCursor;

    public bool gameIsPaused;

    public AudioMixer SFXAudioMixer;
    public AudioMixer MusicAudioMixer;

    private void Awake()
    {
        //pauseMenu = pauseScreen.GetComponent<PauseMenu>();
        Time.timeScale = 1f;
        Time.fixedDeltaTime = 0.02f;
        //if (SceneManager.GetSceneByName("LoadingScene").isLoaded)
        Debug.Log("Loaded Number of Scenes: " + SceneManager.loadedSceneCount);
        //SceneManager.UnloadSceneAsync("LoadingScene");
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
            virtualCursor.GetComponent<VirtualCursorTest>().enabled = false;
            virtualCursor.GetComponent<VirtualMouseInput>().enabled = false;
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            gameIsPaused = false;
            virtualCursor.GetComponent<VirtualCursorTest>().enabled = true;
            virtualCursor.GetComponent<VirtualMouseInput>().enabled = true;
            Cursor.lockState = CursorLockMode.Locked;
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
