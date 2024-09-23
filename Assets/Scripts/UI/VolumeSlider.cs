using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.UI;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
    private enum VolumeType
    {
        MASTER,
        MUSIC,
        SFX,
        MOUSESENS
    }
    [Header("Type")]
    [SerializeField] private VolumeType volumeType;
    [SerializeField] private VirtualMouseInput virtualCursor;

    private Slider volumeSlider;

    string volumeMaster = "VolumeMaster";
    string volumeMusic = "VolumeMusic";
    string volumeSFX = "VolumeSFX";
    string mouseSens = "MouseSens";

    private void Awake()
    {
        //pauseMenuGO.transform.GetChild(1).transform.GetChild(2).transform.GetChild(1).GetComponent<Slider>().value = PlayerPrefs.GetFloat(volumeMusic);
        //pauseMenuGO.transform.GetChild(1).transform.GetChild(3).transform.GetChild(1).GetComponent<Slider>().value = PlayerPrefs.GetFloat(volumeSFX);
        volumeSlider = this.GetComponentInChildren<Slider>();
    }

    // Start is called before the first frame update
    void Start()
    {
        switch (volumeType)
        {
            case VolumeType.MASTER:
                AudioManager.instance.masterVolume = PlayerPrefs.GetFloat(volumeMaster);
                volumeSlider.value = AudioManager.instance.masterVolume;
                break;
            case VolumeType.MUSIC:
                AudioManager.instance.musicVolume = PlayerPrefs.GetFloat(volumeMusic);
                volumeSlider.value = AudioManager.instance.musicVolume;
                break;
            case VolumeType.SFX:
                AudioManager.instance.SFXVolume = PlayerPrefs.GetFloat(volumeSFX);
                volumeSlider.value = AudioManager.instance.SFXVolume;
                break;
            case VolumeType.MOUSESENS:
                virtualCursor.cursorSpeed = PlayerPrefs.GetFloat(mouseSens);
                volumeSlider.value = virtualCursor.cursorSpeed;
                break;
            default:
                Debug.LogWarning("Volume Type Not Supported: " + volumeType);
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        switch (volumeType)
        {
            case VolumeType.MASTER:
                volumeSlider.value = AudioManager.instance.masterVolume;
                break;
            case VolumeType.MUSIC:
                volumeSlider.value = AudioManager.instance.musicVolume;
                break;
            case VolumeType.SFX:
                volumeSlider.value = AudioManager.instance.SFXVolume;
                break;
            case VolumeType.MOUSESENS:
                volumeSlider.value = virtualCursor.cursorSpeed;
                break;
            default:
                Debug.LogWarning("Volume Type Not Supported: " + volumeType);
                break;
        }
    }

    public void OnSliderValueChanged()
    {
        switch (volumeType)
        {
            case VolumeType.MASTER:
                AudioManager.instance.masterVolume = volumeSlider.value;
                PlayerPrefs.SetFloat(volumeMaster, AudioManager.instance.masterVolume);
                PlayerPrefs.Save();
                break;
            case VolumeType.MUSIC:
                AudioManager.instance.musicVolume = volumeSlider.value;
                PlayerPrefs.SetFloat(volumeMusic, AudioManager.instance.musicVolume);
                PlayerPrefs.Save();
                break;
            case VolumeType.SFX:
                AudioManager.instance.SFXVolume = volumeSlider.value;
                PlayerPrefs.SetFloat(volumeSFX, AudioManager.instance.SFXVolume);
                PlayerPrefs.Save();
                break;
            case VolumeType.MOUSESENS:
                virtualCursor.cursorSpeed = volumeSlider.value;
                PlayerPrefs.SetFloat(mouseSens, virtualCursor.cursorSpeed);
                PlayerPrefs.Save();
                break;
            default:
                Debug.LogWarning("Volume Type Not Supported: " + volumeType);
                break;
        }
    }
}
