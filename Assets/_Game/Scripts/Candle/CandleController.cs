using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
/// <summary>
/// Script by Redstonetech125 & Mony Dragon
/// </summary>
public class CandleController : MonoBehaviour
{

    [SerializeField, Header("Length in seconds the candle will last")]
    private float candleLength = 60;
    [SerializeField,Header("Candle Length / this = Candle State| 12 being full"), Tooltip("the animation for the candle is 12 frames so 60 / 5 = 12 you want to match the candle length with a respective number here.")]
    private float candleDivisibleFactor = 5f;
    [SerializeField]
    private CandleColor currentCandleColor = CandleColor.Yellow;
    [SerializeField]
    private Animator anim;

    private Queue<CandleColor> candleChanges = new Queue<CandleColor>();

    /// <summary>
    /// Current state of candle, 12 is new, 0 is out
    /// </summary>
    private int candleState;
    /// <summary>
    /// Seconds counting down
    /// </summary>
    private float timeSeconds;

    /// <summary>
    /// Length of the candle in seconds.
    /// </summary>
    public float CandleLength { get => candleLength; set => candleLength = value; }
    /// <summary>
    /// The factorial the candle length is divided by for the <see cref="candleState"/>
    /// </summary>
    public float CandleDivisibleFactor { get => candleDivisibleFactor; set => candleDivisibleFactor = value; }
    /// <summary>
    /// The current candle color
    /// </summary>
    public CandleColor CurrentCandleColor { get => currentCandleColor; set => currentCandleColor = value; }


    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void Start()
    {
        candleState = (int)(candleLength / candleDivisibleFactor);
        Debug.Log(candleState);
        anim.SetInteger("Candlestate", candleState);
        timeSeconds = 0;
    }

    private void Update()
    {
        //Countdown Time
        if (timeSeconds < candleDivisibleFactor && candleState > 0)
        {
            timeSeconds += Time.deltaTime;
        }
        else if (timeSeconds >= candleDivisibleFactor && candleState > 0)
        {
            timeSeconds = 0;
            candleState -= 1;
            Debug.Log(candleState);
            anim.SetInteger("Candlestate", candleState);


        }
        else if (candleState <= 0)
        {
            Debug.Log("Melted");
        }
    }

    /// <summary>
    /// This method changes the <see cref="currentCandleColor"/> and adds the color to the queue sets the <see cref="anim"/> bools to the respective color.
    /// </summary>
    /// <param name="color"></param>
    public IEnumerator ChangeCandleColor(CandleColor color, float timeToLast)
    {
        candleChanges.Enqueue(color);
        currentCandleColor = color;
        switch (currentCandleColor)
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
            currentCandleColor = CandleColor.Yellow;
        }
    }

    public void Reset()
    {
        candleLength = 60;
        candleDivisibleFactor = 5;
        candleState = (int)(candleLength / candleDivisibleFactor);
        anim.SetInteger("Candlestate", candleState);
        timeSeconds = 0;
    }
}
