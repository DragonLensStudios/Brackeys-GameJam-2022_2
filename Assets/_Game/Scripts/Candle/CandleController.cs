using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DragonLens.BrackeysGameJam2022_2.Candles;
/// <summary>
/// Script by Redstonetech125 & Mony Dragon
/// </summary>
public class CandleController : MonoBehaviour
{
    public CandleData CandleData;

    private Animator anim;

    private Queue<CandleColor> candleChanges = new Queue<CandleColor>();
    
    /// <summary>
    /// Seconds counting down
    /// </summary>
    private float timeSeconds;


    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void Start()
    {
        Reset();
    }

    private void Update()
    {
        //Countdown Time
        if (timeSeconds < CandleData.DivisibleFactor && CandleData.CurrentState > 0)
        {
            timeSeconds += Time.deltaTime;
        }
        else if (timeSeconds >= CandleData.DivisibleFactor && CandleData.CurrentState > 0)
        {
            timeSeconds = 0;
            CandleData.CurrentState -= 1;
            Debug.Log(CandleData.CurrentState);
            anim.SetInteger("Candlestate", CandleData.CurrentState);
        }
        else if (CandleData.CurrentState <= 0)
        {
            Debug.Log("Melted");
        }
    }

    /// <summary>
    /// This method changes the current candle color and adds the color to the queue sets the <see cref="anim"/> bools to the respective color.
    /// </summary>
    /// <param name="color"></param>
    public IEnumerator ChangeCandleColor(CandleColor color, float timeToLast)
    {
        candleChanges.Enqueue(color);
        CandleData.CurrentColor = color;
        switch (CandleData.CurrentColor)
        {
            case CandleColor.Yellow:
                anim.SetBool("Yellow", true);
                anim.SetBool("Red", false);
                anim.SetBool("Purple", false);
                anim.SetBool("Blue", false);
                break;
            case CandleColor.Red:
                anim.SetBool("Yellow", false);
                anim.SetBool("Red", true);
                anim.SetBool("Purple", false);
                anim.SetBool("Blue", false);
                break;
            case CandleColor.Purple:
                anim.SetBool("Yellow", false);
                anim.SetBool("Red", false);
                anim.SetBool("Purple", true);
                anim.SetBool("Blue", false);
                break;
            case CandleColor.Blue:
                anim.SetBool("Yellow", false);
                anim.SetBool("Red", false);
                anim.SetBool("Purple", false);
                anim.SetBool("Blue", true);
                break;
        }
        yield return new WaitForSeconds(timeToLast);
        candleChanges.TryDequeue(out var queueColor);
        if (candleChanges.Count <= 0)
        {
            CandleData.CurrentColor = CandleData.Color;
        }
    }

    public void Reset()
    {
        StopAllCoroutines();
        candleChanges.Clear();

        CandleData.Initialize();
        anim.SetInteger("Candlestate", CandleData.CurrentState);
        timeSeconds = 0;
    }
}
