using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class DestroyOnAnimationComplete : MonoBehaviour
{
    [SerializeField] float delay;
    
    // Start is called before the first frame update
    void Start()
    {
        float animTime = GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length;

        Destroy(gameObject, animTime + delay);
    }
}
