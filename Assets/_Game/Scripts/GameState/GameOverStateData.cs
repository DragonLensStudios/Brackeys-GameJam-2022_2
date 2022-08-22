using System;
using UnityEngine;

namespace DragonLens.BrackeysGameJam2022_2.GameStates
{
    //[CreateAssetMenu(menuName = "Game Jam/Game Over State Data")]
    public class GameOverStateData : ScriptableObject
    {
        public event Action OnGameOver;

        public void NotifyGameOverListeners() {
            OnGameOver?.Invoke();
        }
    }
}
