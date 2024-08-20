using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public AudioMixer mainAudioMixer;
    [SerializeField] GameObject Panel;

    string volumeMain = "Volume";

    private void Awake()
    {
        Panel.transform.GetChild(1).GetComponent<Slider>().value = PlayerPrefs.GetFloat(volumeMain);
    }

    private void Start()
    {
        mainAudioMixer.SetFloat("Volume", PlayerPrefs.GetFloat("Volume"));
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void SetVolume(float volume)
    {
        mainAudioMixer.SetFloat("Volume", volume);
        PlayerPrefs.SetFloat(volumeMain, volume);
        PlayerPrefs.Save();
    }

}

