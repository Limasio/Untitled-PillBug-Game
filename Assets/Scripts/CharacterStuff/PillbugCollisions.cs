using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PillbugCollisions : MonoBehaviour
{
    [SerializeField] float timePenalty;
    [SerializeField] TimerManager timer;
    [SerializeField] Rigidbody2D rigidbody;
    [SerializeField] Vector2 knockback;
    [SerializeField] float hitDelay;
    [SerializeField] GameObject FlyExplosion;
    [SerializeField] GameObject FireFlyExplosion;
    [SerializeField] GameObject FlyBushLeaves;
    [SerializeField] Animator animator;

    [SerializeField] int flashNum;
    [SerializeField] float flashDelay;
    [SerializeField] SpriteRenderer spriteRenderer;
    private float delayCounter;
    private bool canHit;
    private int angVelocityCounter;
    private bool isSpinning;

    // Start is called before the first frame update
    void Start()
    {
        canHit = true;
        isSpinning = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!canHit)
        {
            delayCounter += Time.deltaTime;
            if (delayCounter >= hitDelay)
            {
                canHit = true;
            }
        }
        if (TimerManager.instance.timeLeft <= 30.0f)
        {
            animator.SetBool("isFrantic", true);
        }
        else
        {
            animator.SetBool("isFrantic", false);
        }

    }

    private void FixedUpdate()
    {
        animator.SetFloat("angvelocity", Mathf.Abs(rigidbody.angularVelocity));
        if (Mathf.Abs(rigidbody.angularVelocity) > 600f)
        {
            angVelocityCounter += 1;
        }
        else
        {
            angVelocityCounter = 0;
        }
        if (angVelocityCounter >= 10)
        {
            animator.SetBool("Spinning", true);
            isSpinning = true;
        }
        else
        {
            animator.SetBool("Spinning", false);
            isSpinning = false;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (canHit && collision.gameObject.layer != 14)
        {
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
            Destroy(collision.gameObject);
            
            if (!isSpinning)
            {
                StartCoroutine(InvFlash(flashDelay, flashNum, spriteRenderer));
                AudioManager.instance.PlayOneShot(FMODEvents.instance.playerHurt, this.transform.position);
                rigidbody.velocity = new Vector2(0f, 0f);
                rigidbody.angularVelocity = 100f;
                rigidbody.AddForce(knockback, ForceMode2D.Impulse);
                timer.LoseTime(timePenalty);
                canHit = false;
                delayCounter = 0;
            }
            else
            {
                AudioManager.instance.PlayOneShot(FMODEvents.instance.explosion, this.transform.position);
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("collided with: " + collision.gameObject);
        if (collision.gameObject.layer != 9 && collision.gameObject.layer != 3 && collision.gameObject.layer != 8 && isSpinning)
        {
            
            //Instantiate(DestructionPrefab, collision.gameObject.transform.position, Quaternion.identity);
            //audioSource.PlayOneShot(explode, 0.5f);
            
            if (collision.gameObject.GetComponent<Fly>() != null)
            {
                ScoreManager.instance.AddScore(1700);
                Debug.Log("rolled over: " + collision.gameObject);
                Debug.Log("AddingFlyScore");
                Instantiate(FlyExplosion, collision.gameObject.transform.position, Quaternion.identity);
                AudioManager.instance.PlayOneShot(FMODEvents.instance.explosion, this.transform.position);
                Destroy(collision.gameObject);
            }
            else if (collision.gameObject.GetComponent<FlyBush>() != null)
            {
                ScoreManager.instance.AddScore(2600);
                Debug.Log("rolled over: " + collision.gameObject);
                Debug.Log("AddingFlyBushScore");
                AudioManager.instance.PlayOneShot(FMODEvents.instance.explosion, this.transform.position);
                Instantiate(FlyBushLeaves, collision.gameObject.transform.position, Quaternion.identity);
                Destroy(collision.gameObject);
            }
            else if (collision.gameObject.GetComponent<FireFly>() != null)
            {
                ScoreManager.instance.AddScore(3700);
                Debug.Log("rolled over: " + collision.gameObject);
                Debug.Log("AddingFireFlyScore");
                AudioManager.instance.PlayOneShot(FMODEvents.instance.explosion, this.transform.position);
                Instantiate(FireFlyExplosion, collision.gameObject.transform.position, Quaternion.identity);
                Destroy(collision.gameObject);
            }
            else if (collision.gameObject.GetComponent<LightningBolt>() != null)
            {
                ScoreManager.instance.AddScore(2100);
                AudioManager.instance.PlayOneShot(FMODEvents.instance.explosion, this.transform.position);
                Debug.Log("rolled over: " + collision.gameObject);
                Debug.Log("AddingLightningBoltScore");
                Destroy(collision.gameObject);
            }
            
            
        }
        else if (collision.gameObject.layer == 8 && isSpinning)
        {
            Debug.Log("Destroyed Timer");
            timer.AddTime(false);
            //Instantiate(DestructionPrefab, collision.gameObject.transform.position, Quaternion.identity);
            AudioManager.instance.PlayOneShot(FMODEvents.instance.explosion, this.transform.position);
            Destroy(collision.gameObject);
        }
    }

    IEnumerator InvFlash(float delay, int flashes, SpriteRenderer renderer)
    {
        for (int i = 0; i < flashes; i++)
        {
            renderer.enabled = false;
            yield return new WaitForSeconds(delay);
            renderer.enabled = true;
            yield return new WaitForSeconds(delay);
        }
    }
}
