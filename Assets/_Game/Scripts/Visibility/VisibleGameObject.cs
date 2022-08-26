using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisibleGameObject : MonoBehaviour, IVisibility
{
    [SerializeField]
    protected List<CandleColor> visibleColors,hiddenColors = new();

    protected SpriteRenderer sr;

    public virtual void Hide(CandleColor color)
    {
        if (hiddenColors.Contains(color)) { sr.enabled = false; }
    }

    public virtual void Show(CandleColor color)
    {
        if (visibleColors.Contains(color)) { sr.enabled = true;}
    }

    protected virtual void OnEnable()
    {
        EventManager.onCandleColorChanged += EventManager_onCandleColorChanged;
    }

    protected virtual void OnDisable()
    {
        EventManager.onCandleColorChanged -= EventManager_onCandleColorChanged;
    }

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    protected virtual void Start()
    {
        Hide(CandleColor.Yellow);
        Show(CandleColor.Yellow);
    }

    protected virtual void EventManager_onCandleColorChanged(CandleColor color, float timeToLast)
    {
        Hide(color);
        Show(color);
    }
}
