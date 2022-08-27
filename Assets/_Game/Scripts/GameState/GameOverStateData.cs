using System;
using UnityEngine;

//[CreateAssetMenu(menuName = "Game Jam/Game Over State Data")]
public class GameOverStateData : ScriptableObject
{
    public event Action OnGameOver;

    public void NotifyGameOverListeners() 
    {
        OnGameOver?.Invoke();
    }
}
