using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;
using UnityEngine.SceneManagement;
//using System.Dynamic;

public class AudioManager : MonoBehaviour
{
    [Header("Volume")]
    [Range(0, 1)]
    public float masterVolume = 1;
    [Range(0,1)]
    public float musicVolume = 1;
    [Range(0, 1)]
    public float SFXVolume = 1;

    private Bus masterBus;

    private Bus musicBus;

    private Bus sfxBus;
    
    public static AudioManager instance { get; private set; }

    private List<StudioEventEmitter> eventEmitters;

    private List<EventInstance> eventInstances;

    private EventInstance musicEventInstance;

    string volumeMaster = "VolumeMaster";
    string volumeMusic = "VolumeMusic";
    string volumeSFX = "VolumeSFX";

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Found more than one Audio Manager in the scene.");
        }
        instance = this;

        eventEmitters = new List<StudioEventEmitter>();
        eventInstances = new List<EventInstance>();
    }

    private void Start()
    {
        masterBus = RuntimeManager.GetBus("bus:/");
        musicBus = RuntimeManager.GetBus("bus:/Music");
        sfxBus = RuntimeManager.GetBus("bus:/SFX");
        if (SceneManager.GetActiveScene().name == "MainMenu")
        {
            InitializeMusicMain();
        }
        AudioManager.instance.masterVolume = PlayerPrefs.GetFloat(volumeMaster);
        AudioManager.instance.musicVolume = PlayerPrefs.GetFloat(volumeMusic);
        AudioManager.instance.SFXVolume = PlayerPrefs.GetFloat(volumeSFX);
    }

    private void Update()
    {
        masterBus.setVolume(masterVolume);
        musicBus.setVolume(musicVolume);
        sfxBus.setVolume(SFXVolume);
    }

    public void InitializeMusic()
    {
        musicEventInstance = CreateInstance(FMODEvents.instance.feneticMusic);
        musicEventInstance.start();
    }

    public void StopMusic()
    {
        //musicEventInstance = CreateInstance(FMODEvents.instance.feneticMusic);
        musicEventInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        musicEventInstance.release();
    }

    public void InitializeMusicMain()
    {
        musicEventInstance = CreateInstance(FMODEvents.instance.pbrolloutThemeMusic);
        musicEventInstance.start();
    }

    public StudioEventEmitter InitializeEventEmitter(EventReference eventReference, GameObject emitterGameObject)
    {
        StudioEventEmitter emitter = emitterGameObject.GetComponent<StudioEventEmitter>();
        emitter.EventReference = eventReference;
        eventEmitters.Add(emitter);
        foreach (var x in eventEmitters)
        {
            Debug.Log(x.ToString());
        }
        UnityEngine.Debug.Log("imma kms");
        return emitter;
    }

    private void CleanUp()
    {
        foreach (EventInstance eventInstance in eventInstances)
        {
            eventInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
            eventInstance.release();
        }
        foreach(StudioEventEmitter emitter in eventEmitters)
        {
            emitter.Stop();
        }
    }

    public void PlayOneShot(EventReference sound, Vector3 worldPos)
    {
        RuntimeManager.PlayOneShot(sound, worldPos);
    }

    public EventInstance CreateInstance(EventReference eventReference)
    {
        EventInstance eventInstance = RuntimeManager.CreateInstance(eventReference);
        eventInstances.Add(eventInstance);
        foreach (var x in eventInstances)
        {
            Debug.Log(x.ToString());
        }
        UnityEngine.Debug.Log("imma kms");
        return eventInstance;
    }

    private void OnDestroy()
    {
        CleanUp();
    }
}
