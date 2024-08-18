using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Hitscan_DoubleBarrel : MonoBehaviour
{
    private Vector2 mousePos;
    
    [Header("Main Camera:")]
    public Camera m_camera;

    [Header("Player:")]
    public Transform gunHolderDB;

    [Header("Physics Stuff:")]
    public Rigidbody2D m_rigidbody;
    [SerializeField] float shotgunForce;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        mousePos = m_camera.ScreenToWorldPoint(Input.mousePosition);
    }

    public void Shoot()
    {
        m_rigidbody.AddForce(((Vector2)gunHolderDB.position - mousePos).normalized * shotgunForce);
    }
}
