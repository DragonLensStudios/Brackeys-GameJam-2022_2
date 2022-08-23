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
        if(_candleData == null) Debug.LogError("Candle blueprint not assigned.");

        anim = GetComponent<Animator>();
        if(anim != null) anim.runtimeAnimatorController = _candleData.Anim;
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
        EventManager.CandleColorChanged(color);
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
            CurrentColor = CandleColor.Yellow;
            EventManager.CandleColorChanged(CandleColor.Yellow);
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

    private void EventManager_onCandleColorChanged(CandleColor color)
    {
        switch (color)
        {
            case CandleColor.Yellow:
                _candleData = Resources.Load<CandleData>("ScriptableObjects/CandleYellow");
                break;
            case CandleColor.Red:
                _candleData = Resources.Load<CandleData>("ScriptableObjects/CandleRed");
                break;
            case CandleColor.Purple:
                _candleData = Resources.Load<CandleData>("ScriptableObjects/CandlePurple");
                break;
            case CandleColor.Blue:
                _candleData = Resources.Load<CandleData>("ScriptableObjects/CandleBlue");
                break;
        }
        if (anim != null)
        {
            anim.runtimeAnimatorController = _candleData.Anim;
            anim.SetInteger("Candlestate", CurrentState);

        }

    }
}