using AdrianKovatana.Essentials.FiniteStateMachine;
using UnityEngine;

namespace DragonLens.BrackeysGameJam2022_2.GameStates
{
    public class PlayingState : MonoBehaviour, IState
    {
        private PuzzleStateMachine _stateMachine;
        [SerializeField] private GameOverState _gameOverState;
        [SerializeField] private PauseState _pauseState;

        private PlayerController _player;
        private CandleController _candle;

        private void Awake() {
            _stateMachine = GetComponentInParent<PuzzleStateMachine>();
            if(_stateMachine == null)
                Debug.LogError($"{typeof(PlayingState)} must have a state machine to work properly.", this);

            _player = FindObjectOfType<PlayerController>();
            if(_player == null)
                Debug.LogError($"{typeof(PlayingState)} must have a player controller to work properly.", this);

            _candle = FindObjectOfType<CandleController>();
            if(_candle == null)
                Debug.LogError($"{typeof(PlayingState)} must have a candle controller to work properly.", this);
        }

        public void StateEnter() {
            _player.enabled = true;
        }

        public void StateUpdate() {
            if(_candle.CurrentState <= 0) {
                _stateMachine.ChangeState(_gameOverState);
                return;
            }

            // Monitor game pause request
            if(_pauseState.Data.ShouldBePaused) _stateMachine.ChangeState(_pauseState);
        }

        public void StateFixedUpdate() {}

        public void StateExit() {
            _player.enabled = false;
        }
    }
}
