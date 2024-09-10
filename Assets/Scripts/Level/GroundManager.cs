using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundManager : MonoBehaviour
{
    [SerializeField] GameObject ground1;
    [SerializeField] GameObject ground2;
    [SerializeField] GameObject ground3;
    [SerializeField] GameObject ground4;
    [SerializeField] GameObject player;
    [SerializeField] GameObject parentGameObject;
    //[SerializeField] Transform plat3;

    [SerializeField] int numberOfObjects;
    [SerializeField] float recycleOffset;
    [SerializeField] Vector2 startPosition;
    [SerializeField] Vector2 minSize, maxSize, minGap, maxGap;
    [SerializeField] float minY, maxY;

    private Vector2 nextPosition;
    private Queue<GameObject> objectQueue;

    private void Start()
    {
        objectQueue = new Queue<GameObject>(numberOfObjects);
        for (int i = 0; i < numberOfObjects; i++)
        {
            float randomNumber = Random.Range(0.0f, 100.0f);
            if (randomNumber <= 25.0f)
            {
                //objectQueue.Enqueue((Transform)Instantiate(ground1));
                GameObject groundClone = Instantiate(ground1);
                groundClone.transform.SetParent(parentGameObject.transform);
                objectQueue.Enqueue(groundClone);
            }
            else if (randomNumber <= 50.0f)
            {
                //objectQueue.Enqueue((Transform)Instantiate(ground2));
                GameObject groundClone = Instantiate(ground2);
                groundClone.transform.SetParent(parentGameObject.transform);
                objectQueue.Enqueue(groundClone);
            }
            else if (randomNumber <= 75.0f)
            {
                //objectQueue.Enqueue((Transform)Instantiate(ground2));
                GameObject groundClone = Instantiate(ground3);
                groundClone.transform.SetParent(parentGameObject.transform);
                objectQueue.Enqueue(groundClone);
            }
            else
            {
                //objectQueue.Enqueue((Transform)Instantiate(ground4));
                GameObject groundClone = Instantiate(ground4);
                groundClone.transform.SetParent(parentGameObject.transform);
                objectQueue.Enqueue(groundClone);
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
        if (objectQueue.Peek().transform.localPosition.x + recycleOffset < player.transform.localPosition.x) //Checking against total distance traveled by player
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

        GameObject o = objectQueue.Dequeue();
        o.transform.localScale = scale;
        o.transform.localPosition = position;
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
