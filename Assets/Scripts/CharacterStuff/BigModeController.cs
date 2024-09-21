using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class BigModeController : MonoBehaviour
{
    [SerializeField] Rigidbody2D rigidbody;
    [SerializeField] Rigidbody rigidbody3D;
    [SerializeField] Collider2D collider;
    [SerializeField] float boostForce;
    [SerializeField] float groundCheckOffset;
    [SerializeField] float groundCheckRadius;
    [SerializeField] float growSize;
    [SerializeField] float growTime;
    private float growTimeCounter;
    private float shrinkTimeCounter;
    private bool isShrinking = false;
    [SerializeField] float shrinkTime;
    [SerializeField] PlayerModeManager playerMode;
    [SerializeField] CinemachineVirtualCamera camera;
    [SerializeField] float cameraMax;
    [SerializeField] float cameraXMax;
    [SerializeField] Vector2 persistentBoost;
    [SerializeField] Vector2 shrinkBoost;
    [SerializeField] TimerManager timer;
    [SerializeField] GameObject background;
    [SerializeField] GameObject DestructionPrefab;
    [SerializeField] GameObject FlyExplosion;
    [SerializeField] GameObject FlyBushLeaves;
    [SerializeField] GameObject FireFlyExplosion;
    [SerializeField] GameObject SlantPlatGibs;
    [SerializeField] GameObject HorizPlatGibs;
    [SerializeField] AudioClip explode;
    //AudioSource audioSource;


    // Start is called before the first frame update
    void Start()
    {
        //audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!isShrinking)//IsGrounded())
        {
            rigidbody.AddForce(persistentBoost);
            //rigidbody3D.AddForce(persistentBoost);
        }
        else
        {
            rigidbody.AddForce(shrinkBoost);
            //rigidbody3D.AddForce(shrinkBoost);
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
        Vector3 initialBackgroundSize = background.transform.localScale;
        float initialCam = camera.m_Lens.OrthographicSize;
        CinemachineFramingTransposer transposer = camera.GetCinemachineComponent<CinemachineFramingTransposer>();
        float initialCamX = transposer.m_ScreenX;
        while (growTimeCounter < growTime)
        {
            growTimeCounter += Time.deltaTime;
            transform.localScale = Vector3.Lerp(initialSize, new Vector3(growSize, growSize, growSize), growTimeCounter / growTime);
            background.transform.localScale = Vector3.Lerp(initialBackgroundSize, new Vector3(2.5f, 2.5f, 2.5f), growTimeCounter / growTime);
            camera.m_Lens.OrthographicSize = Mathf.Lerp(initialCam, cameraMax, growTimeCounter / growTime);
            transposer.m_ScreenX = Mathf.Lerp(initialCamX, cameraXMax, growTimeCounter / growTime);
            yield return null;
        }
    }

    public void ShrinkDisable()
    {
        Debug.Log("disabled");
        isShrinking = false;
        shrinkTimeCounter = 0;
        StartCoroutine(Shrink());
    }

    IEnumerator Shrink()
    {
        isShrinking = true;
        Vector3 initialSize = transform.localScale;
        Vector3 initialBackgroundSize = background.transform.localScale;
        float initialCam = camera.m_Lens.OrthographicSize;
        CinemachineFramingTransposer transposer = camera.GetCinemachineComponent<CinemachineFramingTransposer>();
        float initialCamX = transposer.m_ScreenX;
        while (shrinkTimeCounter < shrinkTime)
        {
            shrinkTimeCounter += Time.deltaTime;
            transform.localScale = Vector3.Lerp(initialSize, new Vector3(1.5f, 1.5f, 1.5f), shrinkTimeCounter / shrinkTime);
            background.transform.localScale = Vector3.Lerp(initialBackgroundSize, new Vector3(1f, 1f, 1f), shrinkTimeCounter / shrinkTime);
            camera.m_Lens.OrthographicSize = Mathf.Lerp(initialCam, 12f, shrinkTimeCounter / shrinkTime);
            transposer.m_ScreenX = Mathf.Lerp(initialCamX, 0.5f, shrinkTimeCounter / shrinkTime);
            yield return null;
        }
        isShrinking = false;
        playerMode.SwapBack();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("collided with: " + collision.gameObject);
        if (collision.gameObject.layer != 9 && collision.gameObject.layer != 3 && collision.gameObject.layer != 8)
        {
            Debug.Log("rolled over: " + collision.gameObject);
            //Instantiate(DestructionPrefab, collision.gameObject.transform.position, Quaternion.identity);
            //audioSource.PlayOneShot(explode, 0.5f);
            AudioManager.instance.PlayOneShot(FMODEvents.instance.explosion, this.transform.position);
            if (collision.gameObject.GetComponent<Fly>() != null)
            {
                ScoreManager.instance.AddScore(1700);
                Debug.Log("AddingFlyScore");
                Instantiate(FlyExplosion, collision.gameObject.transform.position, Quaternion.identity);
            }
            else if (collision.gameObject.GetComponent<FlyBush>() != null)
            {
                ScoreManager.instance.AddScore(2600);
                Debug.Log("AddingFlyBushScore");
                Instantiate(FlyBushLeaves, collision.gameObject.transform.position, Quaternion.identity);
            }
            else if (collision.gameObject.GetComponent<FireFly>() != null)
            {
                ScoreManager.instance.AddScore(3700);
                Debug.Log("AddingFireFlyScore");
                Instantiate(FireFlyExplosion, collision.gameObject.transform.position, Quaternion.identity);
            }
            else if (collision.gameObject.GetComponent<LightningBolt>() != null)
            {
                ScoreManager.instance.AddScore(2100);
                Debug.Log("AddingLightningBoltScore");
            }
            else if (collision.gameObject.GetComponent<SlantedPlat>() != null)
            {
                Instantiate(SlantPlatGibs, collision.gameObject.transform.position, Quaternion.identity);
            }
            else if (collision.gameObject.GetComponent<HorizontalPlat>() != null)
            {
                Instantiate(HorizPlatGibs, collision.gameObject.transform.position, Quaternion.identity);
            }
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.layer == 8)
        {
            Debug.Log("Destroyed Timer");
            timer.AddTime(false);
            //Instantiate(DestructionPrefab, collision.gameObject.transform.position, Quaternion.identity);
            AudioManager.instance.PlayOneShot(FMODEvents.instance.explosion, this.transform.position);
            Destroy(collision.gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        //Instantiate(DestructionPrefab, other.gameObject.transform.position, Quaternion.identity);
        if (collision.gameObject.GetComponent<Fly>() != null)
        {
            ScoreManager.instance.AddScore(1700);
            Debug.Log("AddingFlyScore");
            Instantiate(FlyExplosion, collision.gameObject.transform.position, Quaternion.identity);
        }
        else if (collision.gameObject.GetComponent<FlyBush>() != null)
        {
            ScoreManager.instance.AddScore(2600);
            Debug.Log("AddingFlyBushScore");
            Instantiate(FlyBushLeaves, collision.gameObject.transform.position, Quaternion.identity);
        }
        else if (collision.gameObject.GetComponent<FireFly>() != null)
        {
            ScoreManager.instance.AddScore(3700);
            Debug.Log("AddingFireFlyScore");
            Instantiate(FireFlyExplosion, collision.gameObject.transform.position, Quaternion.identity);
        }
        else if (collision.gameObject.GetComponent<LightningBolt>() != null)
        {
            ScoreManager.instance.AddScore(2100);
            Debug.Log("AddingLightningBoltScore");
        }
        else if (collision.gameObject.GetComponent<SlantedPlat>() != null)
        {
            Instantiate(SlantPlatGibs, collision.gameObject.transform.position, Quaternion.identity);
        }
        else if (collision.gameObject.GetComponent<HorizontalPlat>() != null)
        {
            Instantiate(HorizPlatGibs, collision.gameObject.transform.position, Quaternion.identity);
        }
        AudioManager.instance.PlayOneShot(FMODEvents.instance.explosion, this.transform.position);
        Destroy(collision.gameObject);
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
