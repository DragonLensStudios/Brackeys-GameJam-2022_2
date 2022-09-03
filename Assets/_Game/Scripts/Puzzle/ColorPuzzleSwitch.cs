using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using UnityEngine.Rendering.Universal;

public class ColorPuzzleSwitch : MonoBehaviour
{
    [SerializeField]
    private string switchName, sfxActivateSound, sfxDeactivateSound;
    [SerializeField]
    private CandleColor candleColor;
    [SerializeField]
    private Sprite offSprite, onSprite;
    [SerializeField]
    private bool isEnabled, isActivated;
    [SerializeField]
    private float timeActivate;
    [SerializeField]
    private Light2D light2D;
    [SerializeField] 
    private GameObject activatePopup;


    private ColorPuzzleController puzzleController;
    private SpriteRenderer sr;
    private PlayerController pc;
    private Animator anim;

    public string SwitchName { get => switchName; set => switchName = value; }
    public string SfxActivateSound { get => sfxActivateSound; set => sfxActivateSound = value; }
    public string SfxDeactivateSound { get => sfxDeactivateSound; set => sfxDeactivateSound = value; }
    public CandleColor CandleColor { get => candleColor; set => candleColor = value; }
    public Sprite OffSprite { get => offSprite; set => offSprite = value; }
    public Sprite OnSprite { get => onSprite; set => onSprite = value; }
    public bool IsEnabled { get => isEnabled; set => isEnabled = value; }
    public bool IsActivated { get => isActivated; set => isActivated = value; }
    public SpriteRenderer Sr { get => sr; set => sr = value; }

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        puzzleController = GetComponentInParent<ColorPuzzleController>();
        sr.sprite = offSprite;
        light2D = GetComponent<Light2D>();
    }

    public void Activate(CandleColor color)
    {
        if (candleColor == color)
        {
            isActivated = true;
            if(light2D != null)
            {
                light2D.enabled = true;
            }
            if (sr != null)
            {
                sr.sprite = onSprite;
            }
            if (activatePopup != null)
            {
                activatePopup.SetActive(false);
            }
            StartCoroutine(TimeActivate(timeActivate));
            if (!string.IsNullOrWhiteSpace(sfxActivateSound))
            {
                AudioManager.instance.PlaySound(sfxActivateSound);
            }
        }
    }

    public void Deactivate(CandleColor color)
    {
        if(candleColor == color)
        {
            IsActivated = false;
            if(light2D != null)
            {
                light2D.enabled = false;
            }
            if (sr != null)
            {
                sr.sprite = offSprite;
            }
            if (anim != null)
            {
                anim.SetBool("isLit", false);
            }
            if (!string.IsNullOrWhiteSpace(sfxDeactivateSound))
            {
                AudioManager.instance.PlaySound(sfxDeactivateSound);
            }
        }
    }

    public IEnumerator TimeActivate (float seconds)
    {

        if (pc != null && pc.Anim != null)
        {
            pc.Anim.SetTrigger("Light");

        }
        yield return new WaitForSeconds(seconds);
        if (anim != null)
        {
            anim.SetBool("isLit", true);

        }
        yield return null;
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            pc = collision.GetComponent<PlayerController>();
            if(pc != null)
            {
                if (activatePopup != null && !isActivated && pc.CandleController.CurrentColor == candleColor)
                {
                    activatePopup.SetActive(true);
                }
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
            pc = null;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (pc != null)
            {
                if (pc.IsActivatePressed)
                {
                    Activate(pc.CandleController.CurrentColor);
                    if(puzzleController != null)
                    {
                        EventManager.ColorSwitchActivate(puzzleController.Id, switchName, pc.CandleController.CurrentColor);
                    }
                }
            }
        }
    }
}
