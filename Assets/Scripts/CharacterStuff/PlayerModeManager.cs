using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerModeManager : MonoBehaviour
{
    [SerializeField] GameObject pillbug;
    private Rigidbody2D pillbugRigidbody;
    private Hitscan_GrapplingGun grapplingGun;
    [SerializeField] GameObject bigMode;
    private Rigidbody2D bigModeRigidbody;
    [SerializeField] GameObject cameraObject;
    private CinemachineVirtualCamera camera; 
    [SerializeField] bool isBigMode;
    [SerializeField] int bigCharges;
    [SerializeField] int bigChargeMax;
    [SerializeField] float bigChargeTime;
    [SerializeField] float bigChargeTimeCounter;
    [SerializeField] float bigTime;
    [SerializeField] float bigTimeLeft;
    [SerializeField] float bigModeBoost;

    // Start is called before the first frame update
    void Start()
    {
        pillbugRigidbody = pillbug.GetComponent<Rigidbody2D>();
        grapplingGun = pillbug.GetComponentInChildren<Hitscan_GrapplingGun>();
        bigModeRigidbody = bigMode.GetComponent<Rigidbody2D>();
        camera = cameraObject.GetComponent<CinemachineVirtualCamera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isBigMode)
        {
            if (bigTimeLeft > 0f)
            {
                if (Input.GetKey("space"))
                {
                    bigTimeLeft -= Time.deltaTime;
                }
                else
                {
                    bigTimeLeft = 0f;
                    EndBigMode();
                }
            }
            else 
            {
                EndBigMode();
            }
        }
        else if (Input.GetKey("space") && bigCharges > 0)
        {
            bigTimeLeft = bigTime;
            bigCharges--;
            ActivateBigMode();
        }
        else if (bigCharges < bigChargeMax)
        {
            bigChargeTimeCounter += Time.deltaTime;
            if (bigChargeTimeCounter >= bigChargeTime)
            {
                bigCharges++;
                bigChargeTimeCounter = 0f;
            }
        }
    }

    void ActivateBigMode()
    {
        bigMode.SetActive(true);
        bigMode.transform.position = pillbug.transform.position;
        bigModeRigidbody.velocity = pillbugRigidbody.velocity * bigModeBoost;
        bigModeRigidbody.angularVelocity = pillbugRigidbody.angularVelocity;
        camera.Follow = bigMode.transform;
        if (grapplingGun.grappleRope.enabled) grapplingGun.grappleRope.enabled = false;
        pillbug.SetActive(false);
        isBigMode = true;
    }

    void EndBigMode()
    {
        pillbug.SetActive(true);
        pillbug.transform.position = bigMode.transform.position;
        pillbugRigidbody.velocity = bigModeRigidbody.velocity;
        pillbugRigidbody.angularVelocity = bigModeRigidbody.angularVelocity;
        camera.Follow = pillbug.transform;
        bigMode.SetActive(false);
        isBigMode = false;
    }
}
