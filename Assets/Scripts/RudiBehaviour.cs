using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RudiBehaviour : MonoBehaviour
{
    public UnityAction AppleEaten = () => { };
    private Vector3 InitialSize;
    public UnityAction<bool> GameEnd;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.tag)
        {
            case "Food":
                AppleEaten();
                Grow();
                break;
            case "Enemy":
                Shrink();                
                break;
            case "Ulf":
                Debug.Log("You are both dead... Great Work");
                GameEnd(false);
                break;
            case "Water":
                Debug.Log("You drowned");
                GameEnd(false);
                break;
            default:
                break;
        }
        collision.gameObject.SetActive(false);
    }

    private void Grow()
    {
        transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y + 1.25f,transform.localScale.z);
        Debug.Log("Growing");
        if (transform.localScale.y > 10.6f)
        {
            Debug.Log("You win");
            GameEnd(true);
        }
    }

    private void Shrink()
    {
        transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y - 1.25f, transform.localScale.z);
        Debug.Log("Shrinking");
        if (transform.localScale.y < 1.6f)
        {
            Debug.Log("You are dead");
            GameEnd(false);
        }
    }
    // Use this for initialization
    void Start ()
    {
        InitialSize = transform.localScale;

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Reset()
    {
        transform.localScale = InitialSize;
    }
}
