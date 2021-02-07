using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.PlayerLoop;

public class PlayerController : MonoBehaviour
{
    public LayerMask groundLayers;
    public Transform groundCheck;
    public UnityEvent landEvent;

    const float groundedRadius = .25f;

    private Rigidbody2D rb;
    private bool grounded;
    private bool rightFace;
    private Vector3 velocity = Vector3.zero;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        if (landEvent == null) landEvent = new UnityEvent();
    }

    private void Flip()
    {
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
        rightFace = !rightFace;
    }

	public void Move(float move, bool jump)
	{
        Vector3 targetVelocity = new Vector2(move * 10f, rb.velocity.y);
        rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref velocity, .05f);
        if (move > 0 && !rightFace) Flip();
        else if (move < 0 && rightFace) Flip();
        if (grounded && jump)
        {
           grounded = false;
            rb.AddForce(new Vector2(0f, 1750f));
		}

	}

    private void FixedUpdate()
    {
        bool wasGrounded = grounded;
        grounded = false;

        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheck.position, groundedRadius, groundLayers);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
            {
                grounded = true;
                if (!wasGrounded) 
                    landEvent.Invoke();
            }
        }
    }
}
