using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RudiBehaviour : MonoBehaviour
{
    public UnityAction AppleEaten = () => { };
    public UnityAction<bool> GameEnd;

    public int Step;

    private SkinnedMeshRenderer mesh;
    private int currentSize;


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
        currentSize += Step;
        mesh.SetBlendShapeWeight(0, currentSize);
        if (currentSize >= 100)
        {
            GameEnd(true);
        }
    }

    private void Shrink()
    {
        currentSize -= Step;
        mesh.SetBlendShapeWeight(0, currentSize);
        if (currentSize <= 0)
        {
            GameEnd(false);
        }
    }
    // Use this for initialization
    void Start ()
    {
        currentSize = 0;
        mesh = GetComponentInChildren<SkinnedMeshRenderer>();
        mesh.SetBlendShapeWeight(0, 0);
    }

}
