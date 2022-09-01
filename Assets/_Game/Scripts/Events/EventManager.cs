using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public static event Action<string,string, CandleColor> onCandleColorSwitchActivate;
    public static event Action<CandleColor> onCandleColorChanged;
    public static event Action onCandleOut;
    public static event Action onCandleReset;

    /// <summary>
    /// This method invokes the event <see cref="onCandleColorSwitchActivate"/>
    /// </summary>
    /// <param name="enabled"></param>
    public static void ColorSwitchActivate(string puzzleId, string name, CandleColor color) { onCandleColorSwitchActivate?.Invoke(puzzleId, name, color); }

    /// <summary>
    /// This method invokes the event <see cref="onCandleColorChanged"/>
    /// </summary>
    /// <param name="color"></param>
    public static void CandleColorChanged(CandleColor color) { onCandleColorChanged?.Invoke(color); }

    /// <summary>
    /// This method invokes the event <see cref="onCandleOut"/>
    /// </summary>
    public static void CandleOut() { onCandleOut?.Invoke(); }

    /// <summary>
    /// This method invokes the event <see cref="onCandleReset"/>
    /// </summary>
    public static void CandleReset() { onCandleReset?.Invoke(); }
}
