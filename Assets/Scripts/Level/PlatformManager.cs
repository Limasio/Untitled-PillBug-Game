using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlatformManager : MonoBehaviour
{
    [SerializeField] GameObject plat1;
    [SerializeField] GameObject plat2;
    [SerializeField] GameObject plat3;
    [SerializeField] GameObject plat4a;
    [SerializeField] GameObject plat4b;
    [SerializeField] GameObject plat4c;
    [SerializeField] GameObject plat4d;
    [SerializeField] GameObject plat5;
    [SerializeField] GameObject plat6;
    [SerializeField] GameObject plat7;
    [SerializeField] GameObject plat8;
    [SerializeField] GameObject plat9;
    [SerializeField] GameObject plat10;
    [SerializeField] GameObject player;
    [SerializeField] GameObject player2;

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
        for (int i = 0; i < numberOfObjects; i++)               //There's prob a better way to do this but it runs fine so I don't care :)
        {
            float randomNumber = Random.Range(0.0f, 118.0f);
            if(randomNumber <= 12f)
            {
                objectQueue.Enqueue((GameObject)Instantiate(plat1));
            }
            else if (randomNumber <= 24f)
            {
                objectQueue.Enqueue((GameObject)Instantiate(plat2));
            }
            else if(randomNumber <= 36f)
            {
                objectQueue.Enqueue((GameObject)Instantiate(plat5));
            }
            else if (randomNumber <= 48f)
            {
                objectQueue.Enqueue((GameObject)Instantiate(plat6));
            }
            else if (randomNumber <= 58f && currentTimers < maxTimers && timerSpacingCounter == minTimerSpacing)
            {
                if(Random.Range(0.0f, 4.0f) > 3.0f)
                {
                    objectQueue.Enqueue((GameObject)Instantiate(plat4a));
                    currentTimers++;
                    timerSpacingCounter = 0;
                }
                else if (Random.Range(0.0f, 4.0f) > 2.0f)
                {
                    objectQueue.Enqueue((GameObject)Instantiate(plat4c));
                    currentTimers++;
                    timerSpacingCounter = 0;
                }
                else if (Random.Range(0.0f, 4.0f) > 1.0f)
                {
                    objectQueue.Enqueue((GameObject)Instantiate(plat4d));
                    currentTimers++;
                    timerSpacingCounter = 0;
                }
                else
                {
                    objectQueue.Enqueue((GameObject)Instantiate(plat4b));
                    currentTimers++;
                    timerSpacingCounter = 0;
                }
                
            }
            else if (randomNumber <= 70f)
            {
                objectQueue.Enqueue((GameObject)Instantiate(plat5));
            }
            else if (randomNumber <= 82f)
            {
                objectQueue.Enqueue((GameObject)Instantiate(plat8));
            }
            else if (randomNumber <= 94f)
            {
                objectQueue.Enqueue((GameObject)Instantiate(plat9));
            }
            else if (randomNumber <= 106f)
            {
                objectQueue.Enqueue((GameObject)Instantiate(plat10));
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
        GameObject o = objectQueue.Dequeue();
        Destroy(o);

        Vector2 scale = new Vector2(Random.Range(minSize.x, maxSize.x), Random.Range(minSize.y, maxSize.y));
        Vector2 position = nextPosition;
        position.x += scale.x * 0.5f;
        position.y += scale.y * 0.5f;

        float randomNumber = Random.Range(0.0f, 100.0f);
        if (randomNumber <= 12f)
        {
            GameObject platClone = Instantiate(plat1, position, Quaternion.identity);
            objectQueue.Enqueue(platClone);
        }
        else if (randomNumber <= 24f)
        {
            GameObject platClone = Instantiate(plat2, position, Quaternion.identity);
            objectQueue.Enqueue(platClone);
        }
        else if (randomNumber <= 36f)
        {
            GameObject platClone = Instantiate(plat5, position, Quaternion.identity);
            objectQueue.Enqueue(platClone);
        }
        else if (randomNumber <= 48f)
        {
            GameObject platClone = Instantiate(plat6, position, Quaternion.identity);
            objectQueue.Enqueue(platClone);
        }
        else if (randomNumber <= 58f && currentTimers < maxTimers && timerSpacingCounter == minTimerSpacing)
        {
            if (Random.Range(0.0f, 4.0f) > 3.0f)
            {
                GameObject platClone = Instantiate(plat4a, position, Quaternion.identity);
                objectQueue.Enqueue(platClone);
                //currentTimers++;
                timerSpacingCounter = 0;
            }
            else if (Random.Range(0.0f, 4.0f) > 2.0f)
            {
                GameObject platClone = Instantiate(plat4c, position, Quaternion.identity);
                objectQueue.Enqueue(platClone);
                //currentTimers++;
                timerSpacingCounter = 0;
            }
            else if (Random.Range(0.0f, 4.0f) > 1.0f)
            {
                GameObject platClone = Instantiate(plat4d, position, Quaternion.identity);
                objectQueue.Enqueue(platClone);
                //currentTimers++;
                timerSpacingCounter = 0;
            }
            else
            {
                GameObject platClone = Instantiate(plat4b, position, Quaternion.identity);
                objectQueue.Enqueue(platClone);
                //currentTimers++;
                timerSpacingCounter = 0;
            }

        }
        else if (randomNumber <= 70f)
        {
            GameObject platClone = Instantiate(plat7, position, Quaternion.identity);
            objectQueue.Enqueue(platClone);
        }
        else if (randomNumber <= 82f)
        {
            GameObject platClone = Instantiate(plat8, position, Quaternion.identity);
            objectQueue.Enqueue(platClone);
        }
        else if (randomNumber <= 94f)
        {
            GameObject platClone = Instantiate(plat9, position, Quaternion.identity);
            objectQueue.Enqueue(platClone);
        }
        else if (randomNumber <= 106f)
        {
            GameObject platClone = Instantiate(plat10, position, Quaternion.identity);
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
