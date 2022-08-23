using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DragonLens.BrackeysGameJam2022_2.Candles;
/// <summary>
/// Script by Redstonetech125 & Mony Dragon
/// </summary>
public class CandleController : MonoBehaviour
{
    [SerializeField]
    private CandleData _candleData;

    private Animator anim;

    private Queue<CandleColor> candleChanges = new Queue<CandleColor>();
    
    /// <summary>
    /// Seconds counting down
    /// </summary>
    private float timeSeconds;

    /// <summary>
    /// The current candle color.
    /// </summary>
    public CandleColor CurrentColor { get; private set; }

    /// <summary>
    /// The current state of candle. 12 = new, 0 = out.
    /// </summary>
    public int CurrentState { get; private set; }

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
        if (timeSeconds < _candleData.DivisibleFactor && CurrentState > 0)
        {
            timeSeconds += Time.deltaTime;
        }
        else if (timeSeconds >= _candleData.DivisibleFactor && CurrentState > 0)
        {
            timeSeconds = 0;
            CurrentState -= 1;
            Debug.Log(CurrentState);
            anim.SetInteger("Candlestate", CurrentState);
        }
        else if (CurrentState <= 0)
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
        CurrentColor = color;
        switch (CurrentColor)
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
            CurrentColor = _candleData.Color;
        }
    }

    public void Reset()
    {
        StopAllCoroutines();
        candleChanges.Clear();

        CurrentState = _candleData.MaxStateIndex;
        CurrentColor = _candleData.Color;
        anim.SetInteger("Candlestate", CurrentState);
        timeSeconds = 0;
    }
}
