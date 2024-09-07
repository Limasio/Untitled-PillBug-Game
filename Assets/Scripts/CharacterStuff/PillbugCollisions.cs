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
    }

    void OnTriggerEnter2D(Collider2D hit)
    {
        if (canHit)
        {
            Destroy(hit.gameObject);
            AudioManager.instance.PlayOneShot(FMODEvents.instance.playerHurt, this.transform.position);
            rigidbody.velocity = new Vector2(0f, 0f);
            rigidbody.AddForce(knockback, ForceMode2D.Impulse);
            timer.LoseTime(timePenalty);
            canHit = false;
            delayCounter = 0;
        }
    }
}
