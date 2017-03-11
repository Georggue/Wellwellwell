using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppleGrowth : MonoBehaviour {

    public float TimeTillDrop;
    public float TimeTillDecay;
    private Rigidbody2D rb;
    private bool isFalling = false;
    private IEnumerator AppleLife()
    {
       
        yield return new WaitForSeconds(TimeTillDrop);
        isFalling = true;
        rb.gravityScale = 2;
        rb.GetComponent<Collider2D>().enabled = true;

    }
	// Use this for initialization
	void Start () {
      
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0;
        rb.GetComponent<Collider2D>().enabled = false;
        StartCoroutine(AppleLife());
	}
	
	// Update is called once per frame
	void Update () {
		if(!isFalling)
        {
            rb.velocity = new Vector2(0, 0);
        }
	}
}
