using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillParticle : MonoBehaviour
{
    private ParticleSystem particle;
    private float totalDuration;
    
    // Start is called before the first frame update
    void Start()
    {
        particle = gameObject.GetComponent<ParticleSystem>();
        totalDuration = particle.duration + particle.startLifetime;
        Destroy(gameObject, totalDuration);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
