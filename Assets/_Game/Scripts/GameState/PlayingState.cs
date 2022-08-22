using AdrianKovatana.Essentials.FiniteStateMachine;
using UnityEngine;

namespace DragonLens.BrackeysGameJam2022_2.GameStates
{
    public class PlayingState : MonoBehaviour, IState
    {
        private PuzzleStateMachine _stateMachine;
        [SerializeField] private GameOverState _gameOverState;

        // Temp candle data
        [SerializeField] private float _candleDuration = 5f;
        private float _currentCandleDuration;

        private void Awake() {
            _stateMachine = GetComponentInParent<PuzzleStateMachine>();
            if(_stateMachine == null)
                Debug.LogError($"{typeof(PlayingState)} must have a state machine to work properly.");
        }

        public void StateEnter() {
            _currentCandleDuration = _candleDuration;

            // Enable gameplay/character controls
            print("Gameplay/character conrols enabled");
        }

        public void StateUpdate() {
            // Reduce candle timer here?
            _currentCandleDuration -= Time.deltaTime;

            // If not, then monitor the candle's state and change state accordingly
            if(_currentCandleDuration <= 0f) _stateMachine.ChangeState(_gameOverState);
        }

        public void StateFixedUpdate() { }

        public void StateExit() {
            // Disable gameplay/character controls
            print("Gameplay/character conrols disabled");
        }
    }
}