using AdrianKovatana.Essentials.FiniteStateMachine;
using UnityEngine;

namespace DragonLens.BrackeysGameJam2022_2.GameStates
{
    public class PuzzleStateMachine : StateMachine
    {
        [SerializeField] private InitializationState _initializationState;

        private void Start() {
            ChangeState(_initializationState);
        }
    }
}
