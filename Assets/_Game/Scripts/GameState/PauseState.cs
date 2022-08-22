using AdrianKovatana.Essentials.FiniteStateMachine;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace DragonLens.BrackeysGameJam2022_2.GameStates
{
    public class PauseState : MonoBehaviour, IState
    {
        private PuzzleStateMachine _stateMachine;
        [SerializeField] private PlayingState _playingState;
        public PauseStateData Data;
        private List<IPauseable> _listeners;

        private void Awake() {
            _stateMachine = GetComponentInParent<PuzzleStateMachine>();
            if(_stateMachine == null)
                Debug.LogError($"{typeof(PauseState)} must have a state machine to work properly.");

            RegisterListeners();
        }

        public void StateEnter() {
            // Notify listeners
            foreach(IPauseable listener in _listeners) {
                listener.OnGamePaused();
            }
        }

        public void StateUpdate() {
            if(!Data.ShouldBePaused) _stateMachine.ChangeState(_playingState);
        }

        public void StateFixedUpdate() {}

        public void StateExit() {
            // Notify listeners
            foreach(IPauseable listener in _listeners) {
                listener.OnGameUnpaused();
            }
        }

        public void RegisterListeners() {
            _listeners = new();
            var listeners = Resources.FindObjectsOfTypeAll<MonoBehaviour>().OfType<IPauseable>();
            foreach(var listener in listeners) {
                _listeners.Add(listener);
            }
        }
    }
}
