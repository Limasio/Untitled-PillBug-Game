using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class weapon_generic : MonoBehaviour
{
    [SerializeField] UnityEvent onGunFire;
    [SerializeField] UnityEvent onGunAltFire;
    [SerializeField] UnityEvent onGunStopFire;
    [SerializeField] UnityEvent onGunStopAltFire;
    [SerializeField] UnityEvent onGunReload;
    [SerializeField] float fireCooldown;
    [SerializeField] float altFireCooldown;
    [SerializeField] float reloadTime;
    [SerializeField] float clipSize;
    [SerializeField] bool hasClip;
    [SerializeField] bool hasAltFire;

    [SerializeField] bool Automatic;

    public float currentClipSize { get; private set; }
    private float currentCooldown;
    private float currentAltCooldown;
    private float currentReloadTime;
    private bool isReloading;
    private bool reloadRequested;

    private bool holdingAltFire = false;

    //GameManager
    [SerializeField] GameObject gameManager;
    private GameManager gameManagerInstance;

    public bool getHasClip()
    {
        return hasClip;
    }

    public bool getHasAltFire()
    {
        return hasAltFire;
    }

    public float getCurrentClipSize()
    {
        return currentClipSize;
    }

    // Start is called before the first frame update
    void Awake()
    {
        currentCooldown = fireCooldown;
        currentAltCooldown = altFireCooldown;
        currentReloadTime = 0f;
        currentClipSize = clipSize;
        isReloading = false;
        reloadRequested = false;
        gameManagerInstance = gameManager.GetComponent<GameManager>();


    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (!(gameManagerInstance.playerIsLocked || gameManagerInstance.gameIsPaused))
        {
            
            if (Automatic)
            {
                if (Input.GetMouseButton(0))
                {
                    if (currentCooldown <= 0f)
                    {
                        if (currentClipSize > 0 && !isReloading)
                        {
                            onGunFire?.Invoke();
                            currentCooldown = fireCooldown;
                            if (hasClip)
                            {
                                currentClipSize = currentClipSize - 1f;
                            }
                            
                        }
                        else if (currentReloadTime <= 0f && !isReloading)
                        {
                            onGunReload?.Invoke();
                            currentReloadTime = reloadTime;
                            isReloading = true;
                        }
                        else if (currentReloadTime <= 0f && isReloading)
                        {
                            isReloading = false;
                            currentClipSize = clipSize;
                            reloadRequested = false;
                        }

                    }

                }
                else if (Input.GetMouseButton(1) && hasAltFire)
                {
                    if (currentAltCooldown <= 0f)
                    {
                        if (currentClipSize > 0 && !isReloading)
                        {
                            onGunAltFire?.Invoke();
                            currentAltCooldown = altFireCooldown;
                            if (hasClip)
                            {
                                //currentClipSize = currentClipSize - 1f;
                            }

                        }
                        else if (currentReloadTime <= 0f && !isReloading)
                        {
                            onGunReload?.Invoke();
                            currentReloadTime = reloadTime;
                            isReloading = true;
                        }
                        else if (currentReloadTime <= 0f && isReloading)
                        {
                            isReloading = false;
                            currentClipSize = clipSize;
                            reloadRequested = false;
                        }

                    }

                }
                else if (Input.GetKey(KeyCode.R) && currentClipSize < clipSize)
                {
                    reloadRequested = true;
                }
                else
                {
                    if (currentCooldown <= 0f)
                    {
                        if ((reloadRequested || currentClipSize < 1) && currentReloadTime <= 0f && !isReloading)
                        {
                            onGunReload?.Invoke();
                            currentReloadTime = reloadTime;
                            isReloading = true;
                        }
                        else if ((reloadRequested || currentClipSize < 1) && currentReloadTime <= 0f && isReloading)
                        {
                            isReloading = false;
                            currentClipSize = clipSize;
                            reloadRequested = false;
                        }

                    }
                }
            }
            else
            {
                if (Input.GetMouseButtonDown(0))
                {
                    if (currentCooldown <= 0f)
                    {
                        if (currentClipSize > 0 && !isReloading)
                        {
                            onGunFire?.Invoke();
                            currentCooldown = fireCooldown;
                            if (hasClip)
                            {
                                currentClipSize = currentClipSize - 1f;
                            }

                        }
                        else if (currentReloadTime <= 0f && !isReloading)
                        {
                            onGunReload?.Invoke();
                            currentReloadTime = reloadTime;
                            isReloading = true;
                        }
                        else if (currentReloadTime <= 0f && isReloading)
                        {
                            isReloading = false;
                            currentClipSize = clipSize;
                            reloadRequested = false;
                        }

                    }

                }
                else if (Input.GetMouseButtonDown(1) && hasAltFire)
                {
                    if (currentAltCooldown <= 0f)
                    {
                        if (currentClipSize > 0 && !isReloading)
                        {
                            onGunAltFire?.Invoke();
                            holdingAltFire = true;
                            currentAltCooldown = altFireCooldown;
                            if (hasClip)
                            {
                                //currentClipSize = currentClipSize - 1f;
                            }

                        }
                        else if (currentReloadTime <= 0f && !isReloading)
                        {
                            onGunReload?.Invoke();
                            currentReloadTime = reloadTime;
                            isReloading = true;
                        }
                        else if (currentReloadTime <= 0f && isReloading)
                        {
                            isReloading = false;
                            currentClipSize = clipSize;
                            reloadRequested = false;
                        }

                    }

                }
                else if (Input.GetKey(KeyCode.R) && currentClipSize < clipSize)
                {
                    reloadRequested = true;
                }
                else
                {
                    if (currentCooldown <= 0f)
                    {
                        if ((reloadRequested || currentClipSize < 1) && currentReloadTime <= 0f && !isReloading)
                        {
                            onGunReload?.Invoke();
                            currentReloadTime = reloadTime;
                            isReloading = true;
                        }
                        else if ((reloadRequested || currentClipSize < 1) && currentReloadTime <= 0f && isReloading)
                        {
                            isReloading = false;
                            currentClipSize = clipSize;
                            reloadRequested = false;
                        }

                    }
                }
            }

            if (Input.GetMouseButtonUp(0))
            {
                onGunStopFire?.Invoke();
            }
            if (Input.GetMouseButtonUp(1) || (Input.GetMouseButton(1) == false && holdingAltFire == true))
            {
                onGunStopAltFire?.Invoke();
                holdingAltFire = false;
            }

            currentCooldown -= Time.deltaTime;
            currentAltCooldown -= Time.deltaTime;
            currentReloadTime -= Time.deltaTime;
        }

    }
}
