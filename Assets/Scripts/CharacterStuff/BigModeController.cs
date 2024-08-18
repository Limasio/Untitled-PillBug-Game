using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigModeController : MonoBehaviour
{
    [SerializeField] Rigidbody2D rigidbody;
    [SerializeField] Collider2D collider;
    [SerializeField] float boostForce;
    [SerializeField] float groundCheckOffset;
    [SerializeField] float groundCheckRadius;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (IsGrounded())
        {
            rigidbody.AddForce(rigidbody.velocity.normalized * boostForce);
            Debug.Log("boosting");
        }
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
