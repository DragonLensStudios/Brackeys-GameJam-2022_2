using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public static event Action<string, CandleColor> onCandleColorSwitchActivate;
    public static event Action<CandleColor, float> onCandleColorChanged;

    /// <summary>
    /// This method invokes the event <see cref="onCandleColorSwitchActivate"/>
    /// </summary>
    /// <param name="enabled"></param>
    public static void ColorSwitchActivate(string name, CandleColor color) { onCandleColorSwitchActivate?.Invoke(name, color); }

    /// <summary>
    /// This method invokes the event <see cref="onCandleColorChanged"/>
    /// </summary>
    /// <param name="color"></param>
    public static void CandleColorChanged(CandleColor color, float timeToLast = 0) { onCandleColorChanged?.Invoke(color, timeToLast); }
}
