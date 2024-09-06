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
    [SerializeField] GameObject MuzzleFlash;
    [SerializeField] float delay;

    [Header("Physics Stuff:")]
    public Rigidbody2D m_rigidbody;
    [SerializeField] float shotgunForce;

    [Header("Audio Stuff:")]
    //[SerializeField] GameObject music;

    [SerializeField] TimerManager timer;
    [SerializeField] LayerMask targetLayers;
    [SerializeField] float gunDistance;
    [SerializeField] ScoreManager scorer;
    [SerializeField] PlayerModeManager moder;
    
    [SerializeField] Hitscan_GrapplingGun grapplingGun;
    [SerializeField] GameObject DestructionPrefab;

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
        //Play Shotgun Sound
        AudioManager.instance.PlayOneShot(FMODEvents.instance.explosion, this.transform.position);
        
        StartCoroutine(MFlash(delay, MuzzleFlash));
        m_rigidbody.AddForce(((Vector2)gunHolderDB.position - mousePos).normalized * shotgunForce);
        // Debug.Log("gun: " + (Vector2)gunHolderDB.position);
        // Debug.Log("mouse: " + mousePos);
        // Debug.Log("direction: " + (mousePos - (Vector2)gunHolderDB.position).normalized);
        RaycastHit2D hit = Physics2D.Raycast((Vector2)gunHolderDB.position, (mousePos - (Vector2)gunHolderDB.position).normalized, gunDistance, targetLayers);
        if (hit.collider != null)
        {
            GameObject target = hit.collider.gameObject;
            // Debug.Log("hit: " + target);
            if (target.layer == 7 || target.layer == 10)
            {
                // Debug.Log("shot " + target);
                Instantiate(DestructionPrefab, target.gameObject.transform.position, Quaternion.identity);
                Destroy(target);
                if (grapplingGun.grappleRope.enabled) grapplingGun.grappleRope.enabled = false;
            }
            else if(target.layer == 8)
            {
                // Debug.Log("shot " + target);
                timer.AddTime();
                Instantiate(DestructionPrefab, target.gameObject.transform.position, Quaternion.identity);
                Destroy(target);
                if (grapplingGun.grappleRope.enabled) grapplingGun.grappleRope.enabled = false;
            }
            else if(target.layer == 11)
            {
                timer.StartTime();
                scorer.StartScore();
                moder.StartGame();
                AudioManager.instance.InitializeMusic();
                Instantiate(DestructionPrefab, target.gameObject.transform.position, Quaternion.identity);
                Destroy(target);
                if (grapplingGun.grappleRope.enabled) grapplingGun.grappleRope.enabled = false;
            }
        }
    }

    IEnumerator MFlash(float delay, GameObject MuzzleFlash)
    {
        //isCoroutineRunning = true;
        MuzzleFlash.SetActive(true);
        yield return new WaitForSeconds(delay);
        MuzzleFlash.SetActive(false);
        //isCoroutineRunning = false;
    }
}
