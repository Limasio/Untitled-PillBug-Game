using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class FMODEvents : MonoBehaviour
{
    [field: Header("Music")]
    [field: SerializeField] public EventReference feneticMusic { get; private set; }

    [field: SerializeField] public EventReference pbrolloutThemeMusic { get; private set; }

    [field: Header("Explosion/Gun SFX")]
    [field: SerializeField] public EventReference explosion { get; private set; }
    [field: SerializeField] public EventReference grapple { get; private set; }

    [field: Header("Entity SFX")]
    [field: SerializeField] public EventReference radar { get; private set; }

    //The "field:" is there bc when using a public getter and a private setter the Header/SerializeField won't show up in unity w/o it

    public static FMODEvents instance { get; private set; }

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Found more than one FMOD Events instance in the scene.");
        }
        instance = this;
    }
}
