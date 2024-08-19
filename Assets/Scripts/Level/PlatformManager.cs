using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlatformManager : MonoBehaviour
{
    [SerializeField] GameObject plat1;
    [SerializeField] GameObject plat2;
    [SerializeField] GameObject plat3;
    [SerializeField] GameObject plat4;
    [SerializeField] GameObject plat5;
    [SerializeField] GameObject plat6;
    [SerializeField] GameObject plat7;
    [SerializeField] GameObject player;
    [SerializeField] GameObject player2;
    //[SerializeField] Transform plat3;

    [SerializeField] int numberOfObjects;
    [SerializeField] int maxTimers;
    [SerializeField] int minTimerSpacing;
    private int currentTimers;
    private int timerSpacingCounter;
    [SerializeField] float recycleOffset;
    [SerializeField] Vector2 startPosition;
    [SerializeField] Vector2 minSize, maxSize, minGap, maxGap;
    [SerializeField] float minY, maxY;

    private Vector2 nextPosition;
    private Queue<GameObject> objectQueue;

    private void Awake()
    {
        currentTimers = 0;
        timerSpacingCounter = minTimerSpacing;
    }

    private void Start()
    { 
        objectQueue = new Queue<GameObject>(numberOfObjects);
        for (int i = 0; i < numberOfObjects; i++)
        {
            float randomNumber = Random.Range(0.0f, 100.0f);
            if(randomNumber <= 15f)
            {
                objectQueue.Enqueue((GameObject)Instantiate(plat1));
            }
            else if (randomNumber <= 30f)
            {
                objectQueue.Enqueue((GameObject)Instantiate(plat2));
            }
            else if(randomNumber <= 45f)
            {
                objectQueue.Enqueue((GameObject)Instantiate(plat5));
            }
            else if (randomNumber <= 60f)
            {
                objectQueue.Enqueue((GameObject)Instantiate(plat6));
            }
            else if (randomNumber <= 77.7f && currentTimers < maxTimers && timerSpacingCounter == minTimerSpacing)
            {
                objectQueue.Enqueue((GameObject)Instantiate(plat4));
                currentTimers++;
                timerSpacingCounter = 0;
            }
            else if (randomNumber <= 85f)
            {
                objectQueue.Enqueue((GameObject)Instantiate(plat5));
            }
            else
            {
                objectQueue.Enqueue((GameObject)Instantiate(plat3));
            }
            if(timerSpacingCounter != minTimerSpacing)
            {
                timerSpacingCounter++;
            }
        }
        nextPosition = startPosition;
        for (int i = 0; i < numberOfObjects; i++)
        {
            Recycle();
        }
        currentTimers = 0;
    }

    private void Update()
    {
        if(objectQueue.Peek().transform.localPosition.x + recycleOffset < player.transform.localPosition.x || objectQueue.Peek().transform.localPosition.x + recycleOffset < player2.transform.localPosition.x) //Checking against total distance traveled by player
        {
            Recycle2();
            Debug.Log("Ran Recycle2");
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
        else if(nextPosition.y > maxY)
        {
            nextPosition.y = maxY - maxGap.y;
        }
    }

    private void Recycle2()
    {
        objectQueue.Dequeue();

        Vector2 scale = new Vector2(Random.Range(minSize.x, maxSize.x), Random.Range(minSize.y, maxSize.y));
        Vector2 position = nextPosition;
        position.x += scale.x * 0.5f;
        position.y += scale.y * 0.5f;

        float randomNumber = Random.Range(0.0f, 100.0f);
        if (randomNumber <= 15f)
        {
            GameObject platClone = Instantiate(plat1, position, Quaternion.identity);
            objectQueue.Enqueue(platClone);
        }
        else if (randomNumber <= 30f)
        {
            GameObject platClone = Instantiate(plat2, position, Quaternion.identity);
            objectQueue.Enqueue(platClone);
        }
        else if (randomNumber <= 45f)
        {
            GameObject platClone = Instantiate(plat5, position, Quaternion.identity);
            objectQueue.Enqueue(platClone);
        }
        else if (randomNumber <= 60f)
        {
            GameObject platClone = Instantiate(plat6, position, Quaternion.identity);
            objectQueue.Enqueue(platClone);
        }
        else if (randomNumber <= 77.7f && currentTimers < maxTimers && timerSpacingCounter == minTimerSpacing)
        {
            GameObject platClone = Instantiate(plat4, position, Quaternion.identity);
            objectQueue.Enqueue(platClone);
            //currentTimers++;
            timerSpacingCounter = 0;
        }
        else if (randomNumber <= 85f)
        {
            GameObject platClone = Instantiate(plat7, position, Quaternion.identity);
            objectQueue.Enqueue(platClone);
        }
        else
        {
            GameObject platClone = Instantiate(plat3, position, Quaternion.identity);
            objectQueue.Enqueue(platClone);
        }
        if (timerSpacingCounter != minTimerSpacing)
        {
            timerSpacingCounter++;
        }

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
