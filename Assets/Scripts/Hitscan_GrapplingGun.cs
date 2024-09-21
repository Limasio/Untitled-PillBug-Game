using System.Security.Cryptography;
using UnityEngine;
using System.Collections;

public class Hitscan_GrapplingGun : MonoBehaviour
{
    [Header("Scripts Ref:")]
    public GrapplingRope grappleRope;

    [Header("Layers Settings:")]
    [SerializeField] private bool grappleToAll = false;
    [SerializeField] private int grappableLayerNumber = 9;
    [SerializeField] float grappleForce;

    [Header("Main Camera:")]
    public Camera m_camera;

    [Header("Transform Ref:")]
    public Transform gunHolder;
    public Transform gunPivot;
    public Transform firePoint;
    [SerializeField] Animator animator;

    [Header("Physics Ref:")]
    //public SpringJoint2D m_springJoint2D;
    public Rigidbody2D m_rigidbody;
    

    [Header("Rotation:")]
    [SerializeField] private bool rotateOverTime = true;
    [Range(0, 60)][SerializeField] private float rotationSpeed = 4;

    [Header("Distance:")]
    [SerializeField] private bool hasMaxDistance = false;
    [SerializeField] private float maxDistnace = 20;

    private GameObject grappledGO;
    private Vector2 grapplePointOffset;

    private enum LaunchType
    {
        Transform_Launch,
        Physics_Launch
    }

    [Header("Launching:")]
    [SerializeField] private bool launchToPoint = true;
    [SerializeField] private LaunchType launchType = LaunchType.Physics_Launch;
    [SerializeField] private float launchSpeed = 1;

    [Header("No Launch To Point")]
    [SerializeField] private bool autoConfigureDistance = false;
    [SerializeField] private float targetDistance = 3;
    [SerializeField] private float targetFrequncy = 1;

    [HideInInspector] public Vector2 grapplePoint;
    [HideInInspector] public Vector2 grappleDistanceVector;

    [SerializeField] LayerMask targetLayers;

    private bool enableGrapplePhysics;

    private void Start()
    {
        grappleRope.enabled = false;
        enableGrapplePhysics = false;
        //m_springJoint2D.enabled = false;

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1) && TimerManager.instance.hasGameEnded == false && PauseMenu.instance.gameIsPaused == false)
        {
            SetGrapplePoint();
        }
        else if (Input.GetKey(KeyCode.Mouse1) && TimerManager.instance.hasGameEnded == false && PauseMenu.instance.gameIsPaused == false)
        {
            if(grappledGO == null)
            {
                DisableGrapple();
            }
            else if((grapplePointOffset + grapplePoint) != new Vector2(grappledGO.transform.position.x, grappledGO.transform.position.y))
            {
                grapplePoint = (new Vector2(grappledGO.transform.position.x, grappledGO.transform.position.y) - grapplePointOffset);
            }
            
            if (grappleRope.enabled)
            {
                RotateGun(grapplePoint, false);
                animator.SetBool("isFired", true);
            }
            else
            {
                Vector2 mousePos = m_camera.ScreenToWorldPoint(Input.mousePosition);
                RotateGun(mousePos, true);
                animator.SetBool("isFired", false);
            }

            if (launchToPoint && grappleRope.isGrappling)
            {
                if (launchType == LaunchType.Transform_Launch)
                {
                    Vector2 firePointDistnace = firePoint.position - gunHolder.localPosition;
                    Vector2 targetPos = grapplePoint - firePointDistnace;
                    gunHolder.position = Vector2.Lerp(gunHolder.position, targetPos, Time.fixedDeltaTime * launchSpeed);
                }
            }
        }
        else if (Input.GetKeyUp(KeyCode.Mouse1) && TimerManager.instance.hasGameEnded == false && PauseMenu.instance.gameIsPaused == false)
        {
            DisableGrapple();
            //m_springJoint2D.enabled = false;
            //m_rigidbody.gravityScale = 1;
        }
        else if (TimerManager.instance.hasGameEnded == false && PauseMenu.instance.gameIsPaused == false)
        {
            Vector2 mousePos = m_camera.ScreenToWorldPoint(Input.mousePosition);
            RotateGun(mousePos, true);
        }
        
    }

    void RotateGun(Vector3 lookPoint, bool allowRotationOverTime)
    {
        Vector3 distanceVector = lookPoint - gunPivot.position;

        float angle = Mathf.Atan2(distanceVector.y, distanceVector.x) * Mathf.Rad2Deg;
        if (rotateOverTime && allowRotationOverTime)
        {
            gunPivot.rotation = Quaternion.Slerp(gunPivot.rotation, Quaternion.AngleAxis(angle, Vector3.forward), rotationSpeed);
        }
        else
        {
            gunPivot.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }

    void SetGrapplePoint()
    {
        Vector2 distanceVector = m_camera.ScreenToWorldPoint(Input.mousePosition) - gunPivot.position;
        // if (Physics2D.Raycast(firePoint.position, distanceVector.normalized))
        // {
        //     RaycastHit2D _hit = Physics2D.Raycast(firePoint.position, distanceVector.normalized);
        //     if (_hit.transform.gameObject.layer == grappableLayerNumber || grappleToAll)
        //     {
        //         if (Vector2.Distance(_hit.point, firePoint.position) <= maxDistnace || !hasMaxDistance)
        //         {
        //             grapplePoint = _hit.point;
        //             grappleDistanceVector = grapplePoint - (Vector2)gunPivot.position;
        //             grappleRope.enabled = true;
        //         }
        //     }
        // }
        RaycastHit2D _hit = Physics2D.Raycast(firePoint.position, distanceVector.normalized, 500f, targetLayers);
        if (_hit.collider != null)
        {
            if (Vector2.Distance(_hit.point, firePoint.position) <= maxDistnace || !hasMaxDistance)
                {
                    grapplePoint = _hit.point;
                    grappleDistanceVector = grapplePoint - (Vector2)gunPivot.position;
                    grappleRope.enabled = true;
                    grappledGO = _hit.collider.gameObject;
                    grapplePointOffset = new Vector2 (grappledGO.transform.position.x, grappledGO.transform.position.y) - grapplePoint;
                    AudioManager.instance.PlayOneShot(FMODEvents.instance.grapple, this.transform.position);
            }
        }
    }

    private void FixedUpdate()
    {
        if (enableGrapplePhysics)
        {
            m_rigidbody.AddForce((grapplePoint - (Vector2)gunHolder.position).normalized * grappleForce);
        }
    }

    public void DisableGrapple()
    {
        grappleRope.enabled = false;
        animator.SetBool("isFired", false);
        enableGrapplePhysics = false;
    }

    public void Grapple()
    {
        // m_rigidbody.AddForce((grapplePoint - (Vector2)gunHolder.position).normalized * grappleForce);
        enableGrapplePhysics = true;
        //StartCoroutine(applyGrapplingForce());
    }

    protected IEnumerator applyGrapplingForce()
    {
        yield return new WaitForFixedUpdate();
        m_rigidbody.AddForce((grapplePoint - (Vector2)gunHolder.position).normalized * grappleForce);
        //yield break;
    }

    private void OnDrawGizmosSelected()
    {
        if (firePoint != null && hasMaxDistance)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(firePoint.position, maxDistnace);
        }
    }

}
