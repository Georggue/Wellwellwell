using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CleanUp : MonoBehaviour
{
    public UnityAction AppleDestroyed = delegate { };

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(other.tag);
        if (other.CompareTag("Food"))
        {
            AppleDestroyed();
        }
        //todo objectpool
        other.gameObject.SetActive(false);
    }

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }
}