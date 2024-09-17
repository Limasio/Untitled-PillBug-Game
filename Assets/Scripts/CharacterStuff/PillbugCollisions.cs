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
    [SerializeField] Animator animator;
    private float delayCounter;
    private bool canHit;

    // Start is called before the first frame update
    void Start()
    {
        canHit = true;
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
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (canHit)
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
            AudioManager.instance.PlayOneShot(FMODEvents.instance.playerHurt, this.transform.position);
            rigidbody.velocity = new Vector2(0f, 0f);
            rigidbody.AddForce(knockback, ForceMode2D.Impulse);
            timer.LoseTime(timePenalty);
            canHit = false;
            delayCounter = 0;
        }
    }
}
