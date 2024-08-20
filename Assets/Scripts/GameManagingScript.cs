using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class GameManagingScript : MonoBehaviour
{
    public GameObject pauseScreen;
    [SerializeField] private PauseMenu pauseMenu;

    public bool gameIsPaused;

    public AudioMixer mainAudioMixer;

    private void Awake()
    {
        //pauseMenu = pauseScreen.GetComponent<PauseMenu>();
        Time.timeScale = 1f;
    }

    // Start is called before the first frame update
    void Start()
    {
        mainAudioMixer.SetFloat("Volume", PlayerPrefs.GetFloat("Volume"));
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
        Time.timeScale = 1f;
        pauseScreen.SetActive(false);
        SceneManager.LoadScene("MainMenu");
    }

}
