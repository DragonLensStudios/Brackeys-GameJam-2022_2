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
    [SerializeField]
    private bool candleStateFreeze;
<<<<<<< feature/player-animations
<<<<<<< feature/player-animations
    [SerializeField]
    private string refillSfx, candleOutSfx;
=======
>>>>>>> Added Tutorial Level Puzzle Added SaveData for checkpoints Added Lantern
=======
    [SerializeField]
    private string refillSfx, candleOutSfx;
>>>>>>> Added SFX/BGM, Edited scripts to play SFX/BGM accordingly

    private bool isCandleLit = true;
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
    public bool CandleStateFreeze { get => candleStateFreeze; set => candleStateFreeze = value; }

    private void OnEnable()
    {
        EventManager.onCandleColorChanged += EventManager_onCandleColorChanged;
        EventManager.onCandleOut += EventManager_onCandleOut;
    }

    private void OnDisable()
    {
        EventManager.onCandleColorChanged -= EventManager_onCandleColorChanged;
        EventManager.onCandleOut -= EventManager_onCandleOut;
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
<<<<<<< feature/player-animations
<<<<<<< feature/player-animations
        if (!candleStateFreeze && isCandleLit)
=======
        if (!candleStateFreeze)
>>>>>>> Added Tutorial Level Puzzle Added SaveData for checkpoints Added Lantern
=======
        if (!candleStateFreeze && isCandleLit)
>>>>>>> Added SFX/BGM, Edited scripts to play SFX/BGM accordingly
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
<<<<<<< feature/player-animations
<<<<<<< feature/player-animations
                EventManager.CandleOut();
=======
>>>>>>> Added Tutorial Level Puzzle Added SaveData for checkpoints Added Lantern
=======
                EventManager.CandleOut();
>>>>>>> Added SFX/BGM, Edited scripts to play SFX/BGM accordingly
                Debug.Log("Melted");
            }
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
        EventManager.CandleColorChanged(color, timeToLast);
        yield return new WaitForSeconds(timeToLast);
        candleChanges.TryDequeue(out var queueColor);
        if (candleChanges.Count <= 0)
        {
            CurrentColor = CandleColor.Yellow;
            EventManager.CandleColorChanged(CandleColor.Yellow, 0);
        }
    }

    public void Reset()
    {
        StopAllCoroutines();
        candleChanges.Clear();
        isCandleLit = true;
        CurrentState = _candleData.MaxStateIndex;
        CurrentColor = _candleData.Color;
        anim.SetTrigger("Refill");
        anim.SetInteger("Candlestate", CurrentState);
        timeSeconds = 0;
        if (!string.IsNullOrWhiteSpace(refillSfx))
        {
            AudioManager.instance.PlaySound(refillSfx);
        }
    }

    private void EventManager_onCandleColorChanged(CandleColor color, float timeToLast)
    {
        switch (color)
        {
            case CandleColor.Yellow:
                anim.SetBool("Yellow", true);
                anim.SetBool("Red", false);
                anim.SetBool("Purple", false);
                anim.SetBool("Blue", false);
                _candleData = Resources.Load<CandleData>("ScriptableObjects/CandleYellow");
                break;
            case CandleColor.Red:
                anim.SetBool("Yellow", false);
                anim.SetBool("Red", true);
                anim.SetBool("Purple", false);
                anim.SetBool("Blue", false);
                _candleData = Resources.Load<CandleData>("ScriptableObjects/CandleRed");
                break;
            case CandleColor.Purple:
                anim.SetBool("Yellow", false);
                anim.SetBool("Red", false);
                anim.SetBool("Purple", true);
                anim.SetBool("Blue", false);
                _candleData = Resources.Load<CandleData>("ScriptableObjects/CandlePurple");
                break;
            case CandleColor.Blue:
                anim.SetBool("Yellow", false);
                anim.SetBool("Red", false);
                anim.SetBool("Purple", false);
                anim.SetBool("Blue", true);
                _candleData = Resources.Load<CandleData>("ScriptableObjects/CandleBlue");
                break;
        }
        if (anim != null)
        {
            anim.runtimeAnimatorController = _candleData.Anim;
            anim.SetInteger("Candlestate", CurrentState);

        }
    }

    private void EventManager_onCandleOut()
    {
        isCandleLit = false;
        if (!string.IsNullOrWhiteSpace(candleOutSfx))
        {
            AudioManager.instance.PlaySound(candleOutSfx);
        }
    }
}