using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WormCatcher : MonoBehaviour {
    private void OnTriggerEnter2D(Collider2D other)
    {
        var pos = other.gameObject.transform.position;
        other.gameObject.transform.position = new Vector3(pos.x,0.2f,pos.z);
        other.gameObject.GetComponentInChildren<MeshRenderer>().transform.localEulerAngles = (pos.x <0) ? new Vector3(-90, -50, -180) : new Vector3(-90, 50, -180);
        other.gameObject.layer = 9;
        var rb = other.gameObject.GetComponent<Rigidbody2D>();
        rb.gravityScale = 8f;
        other.gameObject.GetComponent<EnemyMovement>().MoveDir = (pos.x < 0) ? new Vector2(1, 0) : new Vector2(-1, 0);
    }

    // Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
