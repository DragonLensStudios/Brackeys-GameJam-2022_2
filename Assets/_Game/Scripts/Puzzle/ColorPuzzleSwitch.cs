using UnityEngine;
using UnityEngine.InputSystem;

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

    private SpriteRenderer sr;
    private PlayerController pc;

    public string SwitchName { get => switchName; set => switchName = value; }
    public string SfxActivateSound { get => sfxActivateSound; set => sfxActivateSound = value; }
    public string SfxDeactivateSound { get => sfxDeactivateSound; set => sfxDeactivateSound = value; }
    public CandleColor CandleColor { get => candleColor; set => candleColor = value; }
    public Sprite OffSprite { get => offSprite; set => offSprite = value; }
    public Sprite OnSprite { get => onSprite; set => onSprite = value; }
    public bool IsEnabled { get => isEnabled; set => isEnabled = value; }
    public bool IsActivated { get => isActivated; set => isActivated = value; }

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        sr.sprite = offSprite;
    }

    public void Activate(CandleColor color)
    {
        if (candleColor == color)
        {
            isActivated = true;
            sr.sprite = onSprite;
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
            sr.sprite = offSprite;
            if (!string.IsNullOrWhiteSpace(sfxDeactivateSound))
            {
                AudioManager.instance.PlaySound(sfxDeactivateSound);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            pc = collision.GetComponent<PlayerController>();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
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
                    EventManager.ColorSwitchActivate(switchName, pc.CandleController.CurrentColor);
                }
            }
        }
    }
}
