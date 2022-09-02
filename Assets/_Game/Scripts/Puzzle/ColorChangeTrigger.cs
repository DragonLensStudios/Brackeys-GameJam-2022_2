using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class ColorChangeTrigger : MonoBehaviour
{
    [SerializeField]
    private CandleColor color;
    [SerializeField]
    private Sprite offSprite, onSprite;
    [SerializeField]
    private string activateSfx;
    [SerializeField]
    private Light2D light2D;
    [SerializeField]
    private GameObject activatePopup;

    private PlayerController pc;
    private SpriteRenderer sr;

    private void OnEnable()
    {
        EventManager.onCandleColorChanged += EventManager_onCandleColorChanged;
    }



    private void OnDisable()
    {
        EventManager.onCandleColorChanged -= EventManager_onCandleColorChanged;
    }

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        sr.sprite = offSprite;
        light2D = GetComponent<Light2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (activatePopup != null)
            {
                activatePopup.SetActive(true);
            }
            pc = collision.GetComponent<PlayerController>();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (activatePopup != null)
            {
                activatePopup.SetActive(false);
            }
            pc = null;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (pc != null)
        {
            if (pc.IsActivatePressed)
            {
                Activate(color);
            }
        }
    }

    public void Activate(CandleColor color)
    {
        if(pc != null)
        {
            if (!string.IsNullOrEmpty(activateSfx))
            {
                AudioManager.instance.PlaySound(activateSfx);
            }
            EventManager.CandleColorChanged(color);
        }
    }

    private void EventManager_onCandleColorChanged(CandleColor color)
    {
        if (this.color == color)
        {
            sr.sprite = onSprite;
            if (light2D != null)
            {
                light2D.enabled = true;
            }
        }
        else
        {
            sr.sprite = offSprite;
            if (light2D != null)
            {
                light2D.enabled = false;
            }
        }
    }

}