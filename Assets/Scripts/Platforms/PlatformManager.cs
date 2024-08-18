using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlatformManager : MonoBehaviour
{
    [SerializeField] Transform plat1;
    [SerializeField] Transform plat2;
    [SerializeField] Transform plat3;
    [SerializeField] GameObject player;
    //[SerializeField] Transform plat3;

    [SerializeField] int numberOfObjects;
    [SerializeField] float recycleOffset;
    [SerializeField] Vector2 startPosition;
    [SerializeField] Vector2 minSize, maxSize, minGap, maxGap;
    [SerializeField] float minY, maxY;

    private Vector2 nextPosition;
    private Queue<Transform> objectQueue;

    private void Start()
    {
        objectQueue = new Queue<Transform>(numberOfObjects);
        for (int i = 0; i < numberOfObjects; i++)
        {
            float randomNumber = Random.Range(0.0f, 100.0f);
            if(randomNumber <= 25.0f)
            {
                objectQueue.Enqueue((Transform)Instantiate(plat1));
            }
            else if (randomNumber <= 50.0f)
            {
                objectQueue.Enqueue((Transform)Instantiate(plat2));
            }
            else
            {
                objectQueue.Enqueue((Transform)Instantiate(plat3));
            } 
        }
        nextPosition = startPosition;
        for (int i = 0; i < numberOfObjects; i++)
        {
            Recycle();
        }
    }

    private void Update()
    {
        if(objectQueue.Peek().localPosition.x + recycleOffset < player.transform.localPosition.x) //Checking against total distance traveled by player
        {
            Recycle();
        }
    }

    private void Recycle()
    {
        Vector2 scale = new Vector2(Random.Range(minSize.x, maxSize.x), Random.Range(minSize.y, maxSize.y));
        Vector2 position = nextPosition;
        position.x += scale.x * 0.5f;
        position.y += scale.y * 0.5f;

        Transform o = objectQueue.Dequeue();
        o.localScale = scale;
        o.localPosition = position;
        objectQueue.Enqueue(o);

        //nextPosition += new Vector2(Random.Range(minGap.x, maxGap.x) + scale.x,
        //    Random.Range(minGap.y, maxGap.y));

        nextPosition += new Vector2(Random.Range(minGap.x, maxGap.x),
            Random.Range(minGap.y, maxGap.y));
        if (nextPosition.y < minY)
        {
            nextPosition.y = minY + maxGap.y;
        }
        else if(nextPosition.y > maxY)
        {
            nextPosition.y = maxY - maxGap.y;
        }
    }
}
