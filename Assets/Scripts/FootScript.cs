using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootScript : MonoBehaviour
{
    private float references = 0;
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy")) return;
        references--;
        Debug.Log(references);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy")) return;

        references++;
        Debug.Log(references);

    }

    public bool isGrounded()
    {
        return (references > 0) ? true : false;
    }

    
}
