using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public Vector2 MoveDir;
    public float MoveSpeed;
    public float TopAngle = 45;

    private Rigidbody2D rb;
    private FootScript fs;
    private bool dotzCooldownActive;
    private bool isDying;

    public float TimeTillDecay;
    public float DotzAngle = 45;
    public float DotzForce = 5;
    private float DotzCooldown = 1;
    private ParticleSystem particles;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        fs = GetComponentInChildren<FootScript>();
        particles = GetComponentInChildren<ParticleSystem>(true);
        particles.gameObject.SetActive(false);
    }

    void Update()
    {
        if ((fs.isGrounded() || fs.transform.position.y < 0) && !dotzCooldownActive && !isDying)
        {
            rb.velocity = new Vector2(MoveDir.x * MoveSpeed, MoveDir.y*MoveSpeed);
        }
        if (isDying)
        {
            rb.velocity = Vector2.zero;
        }
    }

    private IEnumerator WaitTillDeath()
    {
        yield return new WaitForSeconds(TimeTillDecay);
        gameObject.SetActive(false);
    }

    public void Squish()
    {
        if (isDying)
            return;

        transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y / 2, transform.localScale.z);
        isDying = true;
        particles.gameObject.SetActive(true);
        gameObject.GetComponentInChildren<Collider2D>().enabled = false;
        StartCoroutine(WaitTillDeath());
    }

    public void Dotz(Vector2 dotzOrigin)
    {
        if (dotzCooldownActive) return;

        particles.gameObject.SetActive(true);
        Vector2 dotzDirection = Vector2.up;
        if (dotzOrigin.x > transform.position.x)
        {
            dotzDirection = dotzDirection.Rotate(DotzAngle);
        }
        else
        {
            dotzDirection = dotzDirection.Rotate(-DotzAngle);
        }
        rb.AddForce(dotzDirection * DotzForce, ForceMode2D.Impulse);
        StartCoroutine(WaitForDotzCooldown());
    }

    private IEnumerator WaitForDotzCooldown()
    {
        dotzCooldownActive = true;
        yield return new WaitForSeconds(DotzCooldown);
        dotzCooldownActive = false;
        particles.gameObject.SetActive(false);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Food")
        {
            collision.gameObject.SetActive(false);
        }
        else if (collision.collider.tag == "Rudi")
        {
            this.gameObject.SetActive(false);
        }
    }


}
