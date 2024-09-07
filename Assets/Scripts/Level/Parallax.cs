using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Parallax : MonoBehaviour
{
    private float length, startpos;
    [SerializeField] GameObject cam;
    [SerializeField] float parallaxEffect;
    [SerializeField] CinemachineVirtualCamera camera;

    // Start is called before the first frame update
    void Start()
    {
        startpos = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
        Debug.Log(length);
    }

    // Update is called once per frame
    void Update()
    {
        length = GetComponent<SpriteRenderer>().bounds.size.x;
        float temp = (cam.transform.position.x *(1- parallaxEffect));
        float dist = (cam.transform.position.x * parallaxEffect);

        //Debug.Log(temp);
        transform.position = new Vector3(startpos + dist, transform.position.y, transform.position.z);

        //Debug.Log(temp);

        if ((temp > startpos + length) && temp > 20) startpos += length;
        else if (temp < startpos - length) startpos -= length;
    }
}
