using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class AppleGrowth : MonoBehaviour
{
    public float TimeTillRipe = 2f;
    public float TimeTillDrop = 0.5f;
    public float TimeTillRotten = 5f;
    public float DissapearDuration = 0.5f;
    public float NormalGravity = 2;

    [Header("Colors")]
    public Color ColorUnripe;
    public Color ColorRipe;
    public Color ColorRotten;
    public float ColorChangeDuration = 0.2f;

    [Header("Shaking")]
    public float ShakeDuration = 0.5f;
    public float ShakeStrength = 90;
    public int ShakeVibrato = 10;
    public float ShakeRandomness = 90;
    public bool FadeoutShake = true;

    private Rigidbody2D rb;
    private Collider2D col;
    private Material mat;
    private bool attached;

    private IEnumerator AppleLife()
    {
        yield return new WaitForSeconds(TimeTillRipe);
        col.enabled = true;
        mat.DOColor(ColorRipe, ColorChangeDuration);

        yield return new WaitForSeconds(TimeTillDrop - ShakeDuration);
        transform.DOShakeRotation(ShakeDuration, ShakeStrength, ShakeVibrato, ShakeRandomness, FadeoutShake);

        yield return new WaitForSeconds(ShakeDuration);
        Fall();
    }

    private IEnumerator Rot()
    {
        mat.DOColor(ColorRotten, TimeTillRotten);
        yield return new WaitForSeconds(TimeTillRotten);
        transform.DOScale(Vector3.zero, DissapearDuration);
        yield return new WaitForSeconds(DissapearDuration);
        gameObject.SetActive(false);
    }

	void Start ()
    {
        transform.localScale = Vector3.zero;
        transform.DOScale(Vector3.one, TimeTillRipe).SetEase(Ease.Linear);
        rb = GetComponent<Rigidbody2D>();
        col = rb.GetComponent<Collider2D>();
        rb.gravityScale = 0;
        col.enabled = false;

        mat = GetComponentInChildren<Renderer>().material;
        mat.color = ColorUnripe;

        StartCoroutine(AppleLife());
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (attached)
        {
            StopAllCoroutines();
            transform.DOKill();
            Fall();
        }
    }

    private void Fall()
    {
        rb.gravityScale = NormalGravity;
        attached = false;
        StartCoroutine(Rot());
    }
}
