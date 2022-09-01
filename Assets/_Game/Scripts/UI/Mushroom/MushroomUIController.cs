using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushroomUIController : MonoBehaviour
{
    
    [SerializeField] Sprite yellowMushroom, redMushroom, purpleMushroom, blueMushroom;

    private SpriteRenderer sr;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }
    private void OnEnable()
    {
        EventManager.onCandleColorChanged += EventManager_onCandleColorChanged;
    }

    private void OnDisable()
    {
        EventManager.onCandleColorChanged -= EventManager_onCandleColorChanged;
    }

    private void EventManager_onCandleColorChanged(CandleColor color)
    {
        if(sr != null)
        {
            switch (color)
            {
                case CandleColor.Yellow:
                    sr.sprite = yellowMushroom;
                    break;
                case CandleColor.Red:
                    sr.sprite = redMushroom;
                    break;
                case CandleColor.Purple:
                    sr.sprite = purpleMushroom;
                    break;
                case CandleColor.Blue:
                    sr.sprite = blueMushroom;
                    break;
            }
            sr.color = Color.white;
        }
    }
}
