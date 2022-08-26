using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushroomUIController : MonoBehaviour
{
    
    [SerializeField] Sprite yellowMushroom, redMushroom, purpleMushroom, blueMushroom;

    [SerializeField]
    private float remainingTime;

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

    private void Update()
    {
        //if(sr != null)
        //{
        //    Color initColor = sr.color;
        //    Color targetColor = new Color(initColor.r, initColor.g, initColor.b, 0f);

        //    float elapsedTime = 0f;
        //    if(elapsedTime <= remainingTime)
        //    {
        //        sr.color = Color.Lerp(initColor, targetColor, elapsedTime / remainingTime);
        //        elapsedTime += Time.deltaTime;
        //        remainingTime -= Time.deltaTime;
        //    }

        //}

    }

    private void EventManager_onCandleColorChanged(CandleColor color, float timeToLast)
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
            if(timeToLast > 0f)
            {
                sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 255f);
            }
            else
            {
                sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 0f);
            }
        }
        remainingTime = timeToLast;
    }
}
