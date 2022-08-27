using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverMenuController : MonoBehaviour
{
    [SerializeField]
    private Page gameOverPage;
    [SerializeField]
    private GameOverStateData gameOverData;

    private void OnEnable()
    {
        if(gameOverData != null)
        {
            gameOverData.OnGameOver += GameOverData_OnGameOver;
        }
    }

    private void OnDisable()
    {
        if (gameOverData != null)
        {
            gameOverData.OnGameOver -= GameOverData_OnGameOver;
        }
    }

    private void GameOverData_OnGameOver()
    {
        if(gameOverPage != null)
        {
            gameOverPage.Enter(true);
        }
    }
}
