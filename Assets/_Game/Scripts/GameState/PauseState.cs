using AdrianKovatana.Essentials.FiniteStateMachine;
using UnityEditor;
using UnityEngine;

namespace DragonLens.BrackeysGameJam2022_2.GameStates
{
    public class PauseState : MonoBehaviour, IState
    {
        private PuzzleStateMachine _stateMachine;
        [SerializeField] private PlayingState _playingState;
        public PauseStateData Data;

        private void Awake() {
            _stateMachine = GetComponentInParent<PuzzleStateMachine>();
            if(_stateMachine == null)
                Debug.LogError($"{typeof(PauseState)} must have a state machine to work properly.");

            // Find all pauasable objects to register as listeners to on enter state
        }

        public void StateEnter() {
            // Disable all pauseable scripts
            print("Paused");
        }

        public void StateUpdate() {
            if(!Data.ShouldBePaused) _stateMachine.ChangeState(_playingState);
        }

        public void StateFixedUpdate() {}

        public void StateExit() {
            // Enable all pauseable scripts
            print("Unpaused");
        }
    }
}
