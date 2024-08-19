using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundManager : MonoBehaviour
{
    [SerializeField] Transform ground1;
    [SerializeField] Transform ground2;
    [SerializeField] Transform ground3;
    [SerializeField] Transform ground4;
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
            if (randomNumber <= 25.0f)
            {
                objectQueue.Enqueue((Transform)Instantiate(ground1));
            }
            else if (randomNumber <= 50.0f)
            {
                objectQueue.Enqueue((Transform)Instantiate(ground2));
            }
            else if (randomNumber <= 75.0f)
            {
                objectQueue.Enqueue((Transform)Instantiate(ground2));
            }
            else
            {
                objectQueue.Enqueue((Transform)Instantiate(ground4));
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
        if (objectQueue.Peek().localPosition.x + recycleOffset < player.transform.localPosition.x) //Checking against total distance traveled by player
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
        else if (nextPosition.y > maxY)
        {
            nextPosition.y = maxY - maxGap.y;
        }
    }
}
