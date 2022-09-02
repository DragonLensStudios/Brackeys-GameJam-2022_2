using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class CheckpointTrigger : MonoBehaviour
{
    [SerializeField] private string id;
    [SerializeField] private GameDataSO _gameData;
    [SerializeField] private Light2D light2D;
    [SerializeField] private GameObject activatePopup;

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
        activatePopup = transform.GetChild(0).gameObject;
        anim = GetComponent<Animator>();
        light2D = GetComponent<Light2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if(activatePopup != null)
            {
                activatePopup.SetActive(true);
            }
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
            if (activatePopup != null)
            {
                activatePopup.SetActive(false);
            }
            if (candleController != null)
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
            if(light2D != null)
            {
                light2D.enabled = true;
            }
            anim.SetTrigger("Activate");
            anim.SetBool("IsOn", true);
            EventManager.CandleReset();
            candleController.CandleStateFreeze = true;
        }
    }

    public void Deactivate()
    {
        if (anim != null && candleController != null)
        {
            anim.SetBool("IsOn", false);
            if(light2D != null)
            {
                light2D.enabled = false;
            }
            candleController.CandleStateFreeze = false;
        }
    }

}
