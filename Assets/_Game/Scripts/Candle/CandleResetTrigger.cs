using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandleResetTrigger : MonoBehaviour
{
    private CandleController candleController;
    [SerializeField] private GameDataSO _gameData;

    private void Awake()
    {
        candleController = GameObject.FindGameObjectWithTag("Candle").GetComponent<CandleController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(candleController != null)
        {
            candleController.Reset();
            _gameData.SavePuzzleData();
        }
    }

}