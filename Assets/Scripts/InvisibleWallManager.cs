using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvisibleWallManager : MonoBehaviour
{
    [SerializeField] Transform pillbug;
    [SerializeField] Transform bigMode;
    [SerializeField] float maxDistance;
    private float currentDistance;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        currentDistance = -1;
        if (pillbug.gameObject.activeSelf)
        {
            // Debug.Log("pillbug wall move");
            currentDistance = pillbug.position.x - transform.position.x;
        }
        else if (bigMode.gameObject.activeSelf)
        {
            // Debug.Log("bigMode wall move");
            currentDistance = bigMode.position.x - transform.position.x;
        }
        // Debug.Log("wall currentDistance: " + currentDistance);
        if (currentDistance > maxDistance)
        {
            // Debug.Log("wall moved");
            transform.Translate(new Vector3(currentDistance - maxDistance, 0f, 0f));
        }
    }
}
