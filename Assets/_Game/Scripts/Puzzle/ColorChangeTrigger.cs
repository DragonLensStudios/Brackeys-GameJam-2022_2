using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChangeTrigger : MonoBehaviour
{
    [SerializeField]
    private CandleColor color;
    [SerializeField]
    private float timeToLast = 15f;
    [SerializeField]
    private Sprite offSprite, onSprite;

    private bool isAbleToActivate;
    private PlayerController pc;
    private SpriteRenderer sr;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        sr.sprite = offSprite;
        isAbleToActivate = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            pc = collision.GetComponent<PlayerController>();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            pc = null;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (pc != null)
        {
            if (pc.IsActivatePressed && isAbleToActivate)
            {
                Activate(color, timeToLast);
            }
        }
    }

    public void Activate(CandleColor color, float timeToLast)
    {
        sr.sprite = onSprite;
        isAbleToActivate = false;
        if(pc != null)
        {
            StartCoroutine(pc.ChangeCandleColor(color, timeToLast));
            StartCoroutine(Deactivate(timeToLast));
        }
    }

    public IEnumerator Deactivate(float timeToLast)
    {
        yield return new WaitForSeconds(timeToLast);
        sr.sprite = offSprite;
        isAbleToActivate = true;
    }
}
