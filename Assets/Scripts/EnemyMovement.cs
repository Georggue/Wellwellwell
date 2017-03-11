using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour {

    public Vector2 MoveDir;
    public float MoveSpeed;
    private Rigidbody2D rb;
    private FootScript fs;
	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();
        fs = transform.GetChild(1).GetComponent<FootScript>();
	}
	
	// Update is called once per frame
	void Update () {
        if(fs.isGrounded())
        {
            rb.velocity = new Vector2(MoveDir.x * MoveSpeed, rb.velocity.y);
        }
       
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == "Food")
        {
            collision.gameObject.SetActive(false);
        }else if(collision.collider.tag == "Rudi")
        {
            this.gameObject.SetActive(false);
        }
    }


}
