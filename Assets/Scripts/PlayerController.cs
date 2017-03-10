using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private CircleCollider2D collider;
    public Collider2D groundLeftCollider;
    public Collider2D groundRightCollider;
    private bool jumpPressed;
    private bool grounded;
    public float Speed;
    public float JumpStrength;
    public float MinGroundDistance;
    public float RayCastOffset;
    // Use this for initialization
    void Start ()
	{
	    rb = GetComponent<Rigidbody2D>();
	    collider = GetComponent<CircleCollider2D>();

	}

    private void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            jumpPressed = true;
        }
    }
    private void FixedUpdate()
    {
        float horizontalMovement = Input.GetAxis("Horizontal");

       //var rc =  Physics2D.Raycast(transform.position - (Vector3.down * RayCastOffset), Vector2.down,MinGroundDistance, LayerMask.NameToLayer("Default"));
       // Debug.Log(rc.distance);
        if (jumpPressed)
        {
            rb.AddForce(Vector2.up*JumpStrength,ForceMode2D.Impulse);
            jumpPressed = false;
        }
        rb.velocity = new Vector2(horizontalMovement*Speed,rb.velocity.y);
    }

}
