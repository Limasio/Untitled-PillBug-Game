using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;
using UnityEngine.SceneManagement;
//using System.Dynamic;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance { get; private set; }

    private List<StudioEventEmitter> eventEmitters;

    private List<EventInstance> eventInstances;

    private EventInstance musicEventInstance;

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
        if (SceneManager.GetActiveScene().name == "MainMenu")
        {
            InitializeMusicMain();
        }
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
