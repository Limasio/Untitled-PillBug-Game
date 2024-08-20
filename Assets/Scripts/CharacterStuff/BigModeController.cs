using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class BigModeController : MonoBehaviour
{
    [SerializeField] Rigidbody2D rigidbody;
    [SerializeField] Collider2D collider;
    [SerializeField] float boostForce;
    [SerializeField] float groundCheckOffset;
    [SerializeField] float groundCheckRadius;
    [SerializeField] float growSize;
    [SerializeField] float growTime;
    private float growTimeCounter;
    private float shrinkTimeCounter;
    [SerializeField] float shrinkTime;
    [SerializeField] PlayerModeManager playerMode;
    [SerializeField] CinemachineVirtualCamera camera;
    [SerializeField] float cameraMax;
    [SerializeField] float cameraXMax;
    [SerializeField] Vector2 persistentBoost;
    [SerializeField] TimerManager timer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (IsGrounded())
        {
            // rigidbody.AddForce(rigidbody.velocity.normalized * boostForce);
            rigidbody.AddForce(persistentBoost);
            // Debug.Log("boosting");
        }
    }
    
    void OnEnable()
    {
        Debug.Log("enabled");
        growTimeCounter = 0;
        StartCoroutine(Grow());
    }

    IEnumerator Grow()
    {
        Vector3 initialSize = transform.localScale;
        float initialCam = camera.m_Lens.OrthographicSize;
        CinemachineFramingTransposer transposer = camera.GetCinemachineComponent<CinemachineFramingTransposer>();
        float initialCamX = transposer.m_ScreenX;
        while (growTimeCounter < growTime)
        {
            growTimeCounter += Time.deltaTime;
            transform.localScale = Vector3.Lerp(initialSize, new Vector3(growSize, growSize, growSize), growTimeCounter / growTime);
            camera.m_Lens.OrthographicSize = Mathf.Lerp(initialCam, cameraMax, growTimeCounter / growTime);
            transposer.m_ScreenX = Mathf.Lerp(initialCamX, cameraXMax, growTimeCounter / growTime);
            yield return null;
        }
    }

    public void ShrinkDisable()
    {
        Debug.Log("disabled");
        shrinkTimeCounter = 0;
        StartCoroutine(Shrink());
    }

    IEnumerator Shrink()
    {
        Vector3 initialSize = transform.localScale;
        float initialCam = camera.m_Lens.OrthographicSize;
        CinemachineFramingTransposer transposer = camera.GetCinemachineComponent<CinemachineFramingTransposer>();
        float initialCamX = transposer.m_ScreenX;
        while (shrinkTimeCounter < shrinkTime)
        {
            shrinkTimeCounter += Time.deltaTime;
            transform.localScale = Vector3.Lerp(initialSize, new Vector3(1.5f, 1.5f, 1.5f), shrinkTimeCounter / shrinkTime);
            camera.m_Lens.OrthographicSize = Mathf.Lerp(initialCam, 12f, shrinkTimeCounter / shrinkTime);
            transposer.m_ScreenX = Mathf.Lerp(initialCamX, 0.5f, shrinkTimeCounter / shrinkTime);
            yield return null;
        }
        playerMode.SwapBack();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("collided with: " + collision.gameObject);
        if (collision.gameObject.layer != 9 && collision.gameObject.layer != 3 && collision.gameObject.layer != 8)
        {
            Debug.Log("rolled over: " + collision.gameObject);
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.layer == 8)
        {
            Debug.Log("Destroyed Timer");
            timer.AddTime();
            Destroy(collision.gameObject);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        Destroy(other.gameObject);
    }

    bool IsGrounded()
    {
        bool isGrounded = false;
        Vector2 groundPoint = collider.bounds.min;
        groundPoint.y -= groundCheckOffset;
        Collider2D[] groundColliders = Physics2D.OverlapCircleAll(groundPoint, groundCheckRadius);
		for (int i = 0; i < groundColliders.Length; i++)
		{
			if (groundColliders[i].gameObject != gameObject)
			{
				isGrounded = true;
			}
		}
        return isGrounded;
    }
}
