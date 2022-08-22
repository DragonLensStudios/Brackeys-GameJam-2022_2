using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public static event Action<string, CandleColor> onColorSwitchActivate;

    /// <summary>
    /// This method invokes the event <see cref="onColorSwitchActivate"/>
    /// </summary>
    /// <param name="enabled"></param>
    public static void ColorSwitchActivate(string name, CandleColor color) { onColorSwitchActivate?.Invoke(name, color); }
}
