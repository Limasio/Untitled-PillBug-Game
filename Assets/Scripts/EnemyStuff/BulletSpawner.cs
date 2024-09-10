using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    enum SpawnerType { Straight, Spin }

    [Header("Bullet Attributes")]
    [SerializeField] GameObject bullet;
    [SerializeField] float bulletLife = 1f;
    [SerializeField] float speed = 1f;

    [Header("Spawner Attributes")]
    [SerializeField] private SpawnerType spawnerType;
    [SerializeField] private float firingRate = 1f;
    [SerializeField] private GameObject bulletHolder;

    private GameObject spawnedBullet;
    private float timer = 0f;
    private enum BulletType
    {
        FLY,
        LIGHTNING,
        NONE
    }

    [SerializeField] private BulletType bulletType;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (spawnerType == SpawnerType.Spin) transform.eulerAngles = new Vector3(0f, 0f, transform.eulerAngles.z + 1f);
        if(timer >= firingRate)
        {
            Fire();
            timer = 0;
        }
    }

    private void Fire()
    {
        if (bullet)
        {
            spawnedBullet = Instantiate(bullet, transform.position, Quaternion.identity);
            spawnedBullet.GetComponent<Bullets>().bulletSpeed = speed;
            spawnedBullet.GetComponent<Bullets>().bulletLife = bulletLife;
            spawnedBullet.transform.rotation = transform.rotation;
            spawnedBullet.transform.SetParent(bulletHolder.transform);
        }

        switch (bulletType)
        {
            case BulletType.FLY:
                //AudioManager.instance.PlayOneShot(FMODEvents.instance.fireball, this.transform.position);
                break;
            case BulletType.LIGHTNING:
                AudioManager.instance.PlayOneShot(FMODEvents.instance.fireball, this.transform.position);
                break;
            case BulletType.NONE:
                break;
            default:
                Debug.LogWarning("Bullet Type Not Found: " + bulletType);
                break;
        }
    }
}
