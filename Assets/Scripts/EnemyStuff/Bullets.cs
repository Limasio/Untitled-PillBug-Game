using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullets : MonoBehaviour
{
    public float bulletLife;
    public float bulletSpeed;
    [SerializeField] float bulletRotation;

    private Vector2 spawnPoint;
    private float timer = 0f;

    // Start is called before the first frame update
    void Start()
    {
        spawnPoint = new Vector2(transform.position.x, transform.position.y);
    }

    // Update is called once per frame
    void Update()
    {
        if (timer > bulletLife) Destroy(this.gameObject);
        timer += Time.deltaTime;
        transform.position = Movement(timer);
    }

    private Vector2 Movement(float timer)
    {
        float x = timer * bulletSpeed * transform.right.x;
        float y = timer * bulletSpeed * transform.right.y;
        return new Vector2(x+spawnPoint.x, y+spawnPoint.y);
    }
}
