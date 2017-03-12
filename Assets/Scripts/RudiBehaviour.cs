using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RudiBehaviour : MonoBehaviour
{
    public UnityAction AppleEaten = () => { };
    private Vector3 InitialSize;
    public UnityAction<bool> GameEnd;
    public float MaxHeight;
    public int Steps;
    private float stepSize;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.tag)
        {
            case "Food":
                AppleEaten();
                SoundManager.Instance.PlaySound(SoundManager.Sound.RudiEatsGood);
                Grow();
                break;
            case "Enemy":
                SoundManager.Instance.PlaySound(SoundManager.Sound.RudiEatsBad);
                Shrink();                
                break;
            case "Ulf":
                Debug.Log("You are both dead... Great Work");
                GameEnd(false);
                break;
            case "Water":
                Debug.Log("You drowned");
                SoundManager.Instance.PlaySound(SoundManager.Sound.Drowning);
                GameEnd(false);
                return;
            default:
                break;
        }
        collision.gameObject.SetActive(false);
    }

    private void Grow()
    {
        transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y + stepSize, transform.localScale.z);
        Debug.Log("Growing");
        if (transform.localScale.y > MaxHeight)
        {
            Debug.Log("You win");
            GameEnd(true);
        }
    }

    private void Shrink()
    {
        transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y - stepSize, transform.localScale.z);
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
        stepSize = (MaxHeight - 1.6f) / Steps;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Reset()
    {
        transform.localScale = InitialSize;
    }
}
