using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    [Header("Dialogue")]
    [SerializeField] bool levelHasDialogue;
    public bool playerIsLocked;
    //[SerializeField] GameObject dialogueSystem;
    //private DialogueSystem.DialogueHolder dialogueHolders;
    //private int activeDialogueHolder;
    //private bool dialogueIsActive;

    [Header("Pause Screen")]
    public bool gameIsPaused;
    public GameObject pauseScreen;
    //private PauseMenu pauseMenu;

    [Header("Death Screen")]
    public GameObject deathScreen;

    [Header("Audio")]
    public AudioMixer mainAudioMixer;

    //DebugScreen
    GameObject debugScreen;

    // Start is called before the first frame update
    void Awake()
    {
        playerIsLocked = false;
        //dialogueIsActive = false;
    //    pauseMenu = pauseScreen.GetComponent<PauseMenu>();
        debugScreen = GameObject.Find("[Graphy]");
        //GameObject newGO = new GameObject();
        //debugScreen.gameObject.transform.parent = newGO.transform; // NO longer DontDestroyOnLoad();
        Time.timeScale = 1f;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    private void Start()
    {
        if (PlayerPrefs.GetInt("Fullscreen") == 1)
        {
            Screen.SetResolution(PlayerPrefs.GetInt("Resolution Width"), PlayerPrefs.GetInt("Resolution Height"), (Screen.fullScreen = true));
        }
        else
        {
            Screen.SetResolution(PlayerPrefs.GetInt("Resolution Width"), PlayerPrefs.GetInt("Resolution Height"), (Screen.fullScreen = false));
        }

        if (PlayerPrefs.GetInt("Debug Menu") == 1)
        {
            try
            {
                debugScreen.SetActive(true);
            }
            catch
            {
                Debug.Log("Can't find Debug Menu");
            }
        }
        else
        {
            try
            {
                debugScreen.SetActive(false);
            }
            catch
            {
                Debug.Log("Can't find Debug Menu");
            }
        }
        mainAudioMixer.SetFloat("MainVolume", PlayerPrefs.GetFloat("Main Volume"));
    }

    // Update is called once per frame
    void Update()
    {
        
        

    //    if (pauseMenu.gameIsPaused)
    //    {
    //        gameIsPaused = true;
    //    }
    //    else
    //    {
    //        gameIsPaused = false;
    //    }
        
    }

    public void changeLevel(string levelname)
    {
        
        SceneManager.LoadScene(levelname);
    }

    public void playerDeath()
    {
        playerIsLocked = true;
        Destroy(pauseScreen);
        deathScreen.SetActive(true);
        reloadCurrentScene(5f);
    }

    public void reloadCurrentScene(float delay)
    {
        StartCoroutine(reloadSceneCoroutine(delay));
    }

    private IEnumerator reloadSceneCoroutine(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private IEnumerator unlockPlayer(float delay)   //Adding a small delay so that players are less likely to jump after closing the dialogue
    {
        yield return new WaitForSeconds(delay);
        playerIsLocked = false;
    }
}
