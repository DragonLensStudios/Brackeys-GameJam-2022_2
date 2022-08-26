using AdrianKovatana.Essentials.FiniteStateMachine;
using UnityEngine;

namespace DragonLens.BrackeysGameJam2022_2.GameStates
{
    public class InitializationState : MonoBehaviour, IState
    {
        private PuzzleStateMachine _stateMachine;
        [SerializeField] private PlayingState _playingState;

        [SerializeField] private GameDataSO _gameDataToLoad;

        private void Awake() {
            _stateMachine = GetComponentInParent<PuzzleStateMachine>();
            if(_stateMachine == null)
                Debug.LogError($"{typeof(InitializationState)} must have a state machine to work properly.", this);

            if(_gameDataToLoad == null)
                Debug.LogWarning($"{typeof(InitializationState)} was not assigned game data to load.", this);
        }

        public void StateEnter() {
            if(_gameDataToLoad != null) {
                _gameDataToLoad.LoadGameData();
            }
        }

        public void StateUpdate() {
            _stateMachine.ChangeState(_playingState);
        }

        public void StateFixedUpdate() {}
        public void StateExit() {}
    }
}
