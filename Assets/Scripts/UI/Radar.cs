using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Radar : MonoBehaviour
{
    GameObject[] MyListOfObjects;
    [SerializeField] GameObject player;
    [SerializeField] GameObject player2;
    [SerializeField] GameObject wall; //Invisible wall
    [SerializeField] Transform radarPivot;
    [SerializeField] SpriteRenderer spriteRenderer;
    [Range(0, 60)][SerializeField] private float rotationSpeed = 4;

    GameObject closestTimer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(player.activeSelf == true)
        {
            radarPivot.position = player.transform.position;
        }
        else
        {
            radarPivot.position = player2.transform.position;
        }
        FindTimerGameObjects();
        closestTimer = GetClosestObject();
        if(closestTimer != null )
        {
            spriteRenderer.enabled = true;
            RotateRadar(closestTimer.transform.position, false);
        }
        else
        {
            spriteRenderer.enabled = false;
        }
    }

    public void FindTimerGameObjects()
    {
        GameObject[] goArray = FindObjectsOfType(typeof(GameObject)) as GameObject[];
        List<GameObject> goList = new List<GameObject>();
        for (int i = 0; i < goArray.Length; i++)
        {
            if (goArray[i].layer == 8 || goArray[i].layer == 11) { goList.Add(goArray[i]); }
        }
        MyListOfObjects = goList.ToArray();
    }

    GameObject GetClosestObject()
    {
        float closest = 1000;
        GameObject closestObject = null;
        for (int i = 0; i < MyListOfObjects.Length; i++)
        {
            float dist = Vector3.Distance(MyListOfObjects[i].transform.position, player.transform.position);
            if ((dist < closest) && MyListOfObjects[i].transform.position.x >= wall.transform.position.x)
            {
                closest = dist;
                closestObject = MyListOfObjects[i];
            }
        }
        if (closest < 10f)          //yandere dev ass code right here
        {
            if(closest < 7.5f)
            {
                if(closest < 5f)
                {
                    Color tmp = spriteRenderer.color;
                    tmp.a = closest / 100f;
                    spriteRenderer.color = tmp;
                }
                else
                {
                    Color tmp = spriteRenderer.color;
                    tmp.a = closest / 20f;
                    spriteRenderer.color = tmp;
                }
            }
            else
            {
                Color tmp = spriteRenderer.color;
                tmp.a = closest / 10f;
                spriteRenderer.color = tmp;
            }
        }
        else
        {
            Color tmp = spriteRenderer.color;
            tmp.a = 1f;
            spriteRenderer.color = tmp;
        }
        //Debug.Log("Closest: " + closest);
        return closestObject;
    }

    void RotateRadar(Vector3 lookPoint, bool allowRotationOverTime)
    {
        Vector3 distanceVector = lookPoint - radarPivot.position;

        float angle = Mathf.Atan2(distanceVector.y, distanceVector.x) * Mathf.Rad2Deg;
        radarPivot.rotation = Quaternion.Lerp(radarPivot.rotation, Quaternion.AngleAxis(angle, Vector3.forward), rotationSpeed);

    }
}
