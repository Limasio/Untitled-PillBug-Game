using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;

public class PlayerModeManager : MonoBehaviour
{
    [SerializeField] GameObject pillbug;
    private Rigidbody2D pillbugRigidbody;
    private Hitscan_GrapplingGun grapplingGun;
    [SerializeField] GameObject bigMode;
    [SerializeField] BigModeController bigController;
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
    [SerializeField] Vector2 bigModeInitialBoost;
    [SerializeField] float bigModeInitialRotate;
    [SerializeField] Slider bigChargeSlider;
    [SerializeField] Goarrow ArrowScript;
    private bool endingBigMode;
    private bool gameStarted;
    [SerializeField] Image sliderImage;
 
    // Start is called before the first frame update
    void Start()
    {
        pillbugRigidbody = pillbug.GetComponent<Rigidbody2D>();
        grapplingGun = pillbug.GetComponentInChildren<Hitscan_GrapplingGun>();
        bigModeRigidbody = bigMode.GetComponent<Rigidbody2D>();
        camera = cameraObject.GetComponent<CinemachineVirtualCamera>();
        bigChargeSlider.value = bigCharges;
        gameStarted = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameStarted)
        {
            if (isBigMode)
            {
                if (bigTimeLeft > 0f)
                {
                    bigTimeLeft -= Time.deltaTime;
                    bigChargeSlider.value = bigTimeLeft / bigTime;
                }
                else 
                {
                    EndBigMode();
                }
            }
            else if (Input.GetKeyDown("space") && bigCharges > 0 && pillbug.transform.position.x > 212)
            {
                bigTimeLeft = bigTime;
                bigCharges--;
                // bigChargeSlider.value = bigCharges;
                ActivateBigMode();
            }
            else if (Input.GetKeyDown("space") && bigCharges > 0 && pillbug.transform.position.x < 212)
            {
                ArrowScript.flashArrow();
            }
            else if (bigCharges < bigChargeMax)
            {
                bigChargeTimeCounter += Time.deltaTime;
                bigChargeSlider.value = bigChargeTimeCounter / bigChargeTime;
                if (bigChargeTimeCounter >= bigChargeTime)
                {
                    bigCharges++;
                    // bigChargeSlider.value = bigCharges;
                    bigChargeTimeCounter = 0f;
                }
            }
            if (bigCharges > 0)
            {
                sliderImage.color = Color.yellow;
            }
        }
    }

    public void StartGame()
    {
        gameStarted = true;
    }

    void ActivateBigMode()
    {
        bigMode.SetActive(true);
        bigMode.transform.position = pillbug.transform.position;
        // bigModeRigidbody.velocity = pillbugRigidbody.velocity * bigModeBoost;
        // bigModeRigidbody.angularVelocity = pillbugRigidbody.angularVelocity;
        bigModeRigidbody.velocity = new Vector3(0f, 0f, 0f);
        bigModeRigidbody.angularVelocity = 0f;
        camera.Follow = bigMode.transform;
        if (grapplingGun.grappleRope.enabled) grapplingGun.grappleRope.enabled = false;
        pillbug.SetActive(false);
        isBigMode = true;
        bigModeRigidbody.AddForce(bigModeInitialBoost, ForceMode2D.Impulse);
        bigModeRigidbody.AddTorque(bigModeInitialRotate, ForceMode2D.Impulse);
        endingBigMode = false;
    }

    void EndBigMode()
    {
        if (!endingBigMode) bigController.ShrinkDisable();
        endingBigMode = true;
        sliderImage.color = Color.white;
    }

    public void SwapBack()
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
