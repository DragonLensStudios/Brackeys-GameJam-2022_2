using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseButtonPressed : MonoBehaviour
{
    public PauseMenu pauseMenu;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pauseMenu.Pause();
        }
    }
}
