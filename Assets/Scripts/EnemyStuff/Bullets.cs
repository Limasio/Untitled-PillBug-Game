using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullets : MonoBehaviour
{
    public float bulletLife;
    public float bulletSpeed;
    [SerializeField] float bulletRotation;
    [SerializeField] bool customRotation;
    [SerializeField] float bulletSpriteRotation;
    [SerializeField] bool inMainMenu;
    [SerializeField] float teleportOffset;
    [SerializeField] bool killParent;

    private Vector2 spawnPoint;
    private float timer = 0f;
    private bool teleportNeeded;

    private void Awake()
    {
        teleportNeeded = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        //GameObject[] obj = GameObject.FindGameObjectsWithTag("teleport");
        spawnPoint = new Vector2(transform.position.x, transform.position.y);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (timer > bulletLife)
        {
            if (killParent)
            {
                Destroy(this.transform.parent.gameObject);
            }
            else
            {
                Destroy(this.gameObject);
            }
        }
        timer += Time.deltaTime;
        transform.position = Movement();
        if (customRotation)
        {
            gameObject.transform.GetChild(0).gameObject.transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, bulletSpriteRotation);
        }
        
        //transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, 180);
    }



    private Vector2 Movement()
    {
        float x = timer * bulletSpeed * transform.right.x;
        //float x = timer * bulletSpeed * Vector2.right.x;
        //float y = timer * bulletSpeed * Vector2.right.y;
        float y = timer * bulletSpeed * transform.right.y;
        if ((x+spawnPoint.x >= teleportOffset+spawnPoint.x) && inMainMenu && !teleportNeeded)
        {
            //teleportNeeded = true;
            timer = 0f;
            GameObject[] obj = GameObject.FindGameObjectsWithTag("teleport");
            for (int i = 0; i < obj.Length; i++)
            {
                obj[i].transform.position = new Vector3(0, 0, 0); //New Position
            }
            teleportNeeded = false;
            Debug.Log("Teleporting");
            //Physics.SyncTransforms();
        }
        //Debug.Log(spawnPoint.x);
        return new Vector2(x+spawnPoint.x, y+spawnPoint.y);
    }
}
