using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
//Script by Redstonetech125
public class Candle_Controller : MonoBehaviour
{
    //Current state of candle, 12 is new, 0 is out
    private int candlestate;
    //Seconds counting down
    private double time_seconds;
    //Length of candle 
    public float candle_length;
    public Animator animator;
   


    void Start()
    {
        candlestate = Convert.ToInt32((candle_length / 2.5));
        Debug.Log(candlestate);
        animator.SetInteger("Candlestate", candlestate);
        time_seconds = 0;

    }

    // Update is called once per frame
    void Update()
    {
        //Countdown Time
        if (time_seconds < 2.5 && candlestate > 0)
        {
            time_seconds += Time.deltaTime;
        }
        else if (time_seconds >= 2.5 && candlestate > 0)
        {
            time_seconds = 0;
            candlestate -= 1;
            Debug.Log(candlestate);
            animator.SetInteger("Candlestate", candlestate);


        }
        else if (candlestate <= 0)
        {
            Debug.Log("Melted");
        }


    }

}
