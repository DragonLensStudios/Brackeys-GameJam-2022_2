using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointTrigger : MonoBehaviour
{
    [SerializeField] private string id;
    [SerializeField] private GameDataSO _gameData;

    private CandleController candleController;
    private PlayerController pc;
    private Animator anim;
    private bool isActivated;
    private bool isSaved = false;

    public bool IsActivated { get => isActivated; set => isActivated = value; }
    public string Id { get => id; }

    private void Awake()
    {
        candleController = GameObject.FindGameObjectWithTag("Candle").GetComponent<CandleController>();
        anim = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            pc = collision.GetComponent<PlayerController>();
            if (isActivated && candleController != null)
            {
                isSaved = false;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if(candleController != null)
            {
                candleController.CandleStateFreeze = false;
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(pc == null || anim == null || candleController == null) return;

        candleController.CandleStateFreeze = true;
        if (pc.IsActivatePressed) 
        {
            Activate();

            if(!isSaved) {
                print("Saving");
                _gameData.SaveGameData();
                isSaved = true;
            }
        }
    }

    public void Activate()
    {
        if (anim != null && candleController != null)
        {
            isActivated = true;
            anim.SetTrigger("Activate");
            anim.SetBool("IsOn", true);
            candleController.Reset();
            candleController.CandleStateFreeze = true;
        }
    }

    public void Deactivate()
    {
        if (anim != null && candleController != null)
        {
            anim.SetBool("IsOn", false);
            candleController.CandleStateFreeze = false;
        }
    }

}
