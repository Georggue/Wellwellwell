using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootScript : MonoBehaviour
{
    public bool isGrounded = false;
    private float references = 0;
    private void OnTriggerExit2D(Collider2D collision)
    {
        references--;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        references++;
    }


    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
