using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    public Collider2D groundLeftCollider;
    public Collider2D groundRightCollider;
    private FootScript footScript;

    private bool jumpPressed;
    private bool grounded;
    private float horizontalMovement;

    public float Speed;
    public float JumpStrength;
    public float MinGroundDistance;
    public float RayCastOffset;
    public float MinDotzInput = 0.5f;
    private Vector3 InitialPos;
    void Start ()
    {
        InitialPos = transform.position;
        rb = GetComponent<Rigidbody2D>();
        footScript = transform.GetComponentInChildren<FootScript>();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Jump") && footScript.isGrounded())
        {
            jumpPressed = true;
        }
    }
    private void FixedUpdate()
    {
        horizontalMovement = Input.GetAxis("Horizontal");

        if (jumpPressed)
        {
            rb.AddForce(Vector2.up * JumpStrength, ForceMode2D.Impulse);
            jumpPressed = false;
        }

        rb.velocity = new Vector2(horizontalMovement * Speed, rb.velocity.y);
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        EnemyMovement enemy = collision.gameObject.GetComponentInChildren<EnemyMovement>();
        if(enemy != null)
        {
            ContactPoint2D hitPoint = collision.contacts[0];
            float angle = hitPoint.normal.GetAngle();
            if(angle > 360 - enemy.TopAngle || angle < enemy.TopAngle)
            {
                enemy.Squish();
            }
            else if(Mathf.Abs(horizontalMovement) > MinDotzInput)
            {
                enemy.Dotz(transform.position);
            }
        }
    }

    public void Reset()
    {
        transform.position = InitialPos;
    }
}
