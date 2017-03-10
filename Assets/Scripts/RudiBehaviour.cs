using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RudiBehaviour : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.tag)
        {
            case "Food":
                Grow();
                break;
            case "Enemy":
                Shrink();
                break;
            case "Ulf":
                Debug.Log("You are both dead... Great Work");
                break;
            case "Water":
                Debug.Log("You drowned");
                break;
            default:
                break;
        }
    }

    private void Grow()
    {
        transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y + 3.33f,transform.localScale.z);
        Debug.Log("Growing");
        if (transform.localScale.y > 11.6f)
        {
            Debug.Log("You win");
        }
    }

    private void Shrink()
    {
        transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y - 3.33f, transform.localScale.z);
        Debug.Log("Shrinking");
        if (transform.localScale.y < 1.6f)
        {
            Debug.Log("You are dead");
        }
    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
