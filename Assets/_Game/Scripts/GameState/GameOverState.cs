using AdrianKovatana.Essentials.FiniteStateMachine;
using UnityEngine;

namespace DragonLens.BrackeysGameJam2022_2.GameStates
{
    public class GameOverState : MonoBehaviour, IState
    {
        public void StateEnter() {
            // TODO: Notify listeners
            print("Game over");
        }
        public void StateUpdate() {}
        public void StateFixedUpdate() {}
        public void StateExit() {}
    }
}
