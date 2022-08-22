using AdrianKovatana.Essentials.FiniteStateMachine;
using System;
using UnityEngine;

namespace DragonLens.BrackeysGameJam2022_2.GameStates
{
    public class GameOverState : MonoBehaviour, IState
    {
        public GameOverStateData Data;

        public void StateEnter() {
            Data.NotifyGameOverListeners();
            print("Game Over");
        }
        public void StateUpdate() {}
        public void StateFixedUpdate() {}
        public void StateExit() {}
    }
}
