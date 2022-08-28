using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;

public class ColorPuzzleController : MonoBehaviour
{
    [SerializeField]
    private string id;
    [SerializeField]
    private float solveTimeBeforeReset = 10f;
    [SerializeField]
    List<ColorPuzzleSwitch> switches = new List<ColorPuzzleSwitch>();
    [SerializeField]
    private bool hasSolveTime ,isPuzzleStarted, isPuzzleComplete, isOrderRequired;
    [SerializeField]
    private List<string> switchesOrderRequired = new List<string>();
    [SerializeField]
    private string puzzleCompleteSfx, puzzleFailedSfx;
    [SerializeField]
    private UnityEvent activatedSwitchesCallback;

    private float currentTimer;
    private List<ColorPuzzleSwitch> activatedSwitches = new List<ColorPuzzleSwitch>();

    public string Id { get => id; }
    public bool IsPuzzleStarted { get => isPuzzleStarted; set => isPuzzleStarted = value; }
    public bool IsPuzzleComplete { get => isPuzzleComplete; set => isPuzzleComplete = value; }
    public List<ColorPuzzleSwitch> ActivatedSwitches { get => activatedSwitches; set => activatedSwitches = value; }
    public List<ColorPuzzleSwitch> Switches { get => switches; set => switches = value; }

    private void OnEnable()
    {
        EventManager.onCandleColorSwitchActivate += EventManager_onColorSwitchActivate;
    }

    private void OnDisable()
    {
        EventManager.onCandleColorSwitchActivate -= EventManager_onColorSwitchActivate;
    }

    private void Awake()
    {
        if(switches.Count <= 0 && transform.childCount > 0)
        {
            switches = GetComponentsInChildren<ColorPuzzleSwitch>().ToList();
        }
    }

    private void Update()
    {
        if (isPuzzleStarted && !isPuzzleComplete && hasSolveTime)
        {
            currentTimer += Time.deltaTime;
            if(currentTimer >= solveTimeBeforeReset)
            {
                ResetPuzzle();
            }
        }
    }

    public void Activate(string puzzleId)
    {
        if(puzzleId == id)
        {
            if(id == "ColorPuzzleTutorial")
            {
                AchievementManager.instance.AddAchievementProgress("TutorialComplete", 1);
            }
            isPuzzleComplete = true;
            activatedSwitches.ForEach(x => x.Sr.enabled = true);
            activatedSwitches.ForEach(x => x.Activate(x.CandleColor));
            activatedSwitches.ForEach(x => x.GetComponent<VisibleGameObject>().enabled = false);
            if (!string.IsNullOrWhiteSpace(puzzleCompleteSfx))
            {
                AudioManager.instance.PlaySound(puzzleCompleteSfx);
            }
            activatedSwitchesCallback.Invoke();
        }
    }

    public void ResetPuzzle()
    {
        for (int i = 0; i < activatedSwitches.Count; i++)
        {
            activatedSwitches[i].Deactivate(activatedSwitches[i].CandleColor);
        }
        activatedSwitches.Clear();
        isPuzzleStarted = false;
        currentTimer = 0;
        if (!string.IsNullOrWhiteSpace(puzzleFailedSfx))
        {
            AudioManager.instance.PlaySound(puzzleFailedSfx);
        }
    }

    private void EventManager_onColorSwitchActivate(string puzzleId, string name, CandleColor color)
    {
        var colorSwitch = switches.Find(x => x.SwitchName == name);
        if(colorSwitch != null)
        {
            if (colorSwitch.IsActivated)
            {
                isPuzzleStarted = true;
                if (!activatedSwitches.Contains(colorSwitch))
                {
                    activatedSwitches.Add(colorSwitch);
                    if (isOrderRequired)
                    {
                        for (int i = 0; i < activatedSwitches.Count; i++)
                        {
                            if(activatedSwitches[i].SwitchName != switchesOrderRequired[i])
                            {
                                ResetPuzzle();
                                return;
                            } 
                        }
                    }
                    
                }
            }
        }
        if(activatedSwitches.Count >= switches.Count)
        {
            if(activatedSwitches.All(x=> x.IsActivated))
            {
                Activate(puzzleId);
            }
        }
    }
}
