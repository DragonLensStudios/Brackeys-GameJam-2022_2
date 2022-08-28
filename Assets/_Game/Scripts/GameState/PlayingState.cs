using UnityEngine;

public class PlayingState : MonoBehaviour, IState
{
    private PuzzleStateMachine _stateMachine;
    [SerializeField] private GameOverState _gameOverState;
    [SerializeField] private PauseState _pauseState;
    public PlayingStateData Data;
    public SpiritOrbSpawnerData OrbSpawnerData;

    private PlayerController _player;
    private CandleController _candle;
    private SpiritOrbSpawner _orbSpawner;

    private void Awake() 
    {
        _stateMachine = GetComponentInParent<PuzzleStateMachine>();
        if(_stateMachine == null)
            Debug.LogError($"{typeof(PlayingState)} must have a state machine to work properly.", this);

        _player = FindObjectOfType<PlayerController>();
        if(_player == null)
            Debug.LogError($"{typeof(PlayingState)} must have a player controller to work properly.", this);

        _candle = FindObjectOfType<CandleController>();
        if(_candle == null)
            Debug.LogError($"{typeof(PlayingState)} must have a candle controller to work properly.", this);

        _orbSpawner = FindObjectOfType<SpiritOrbSpawner>();
        if(_orbSpawner == null)
            Debug.LogError($"{typeof(PlayingState)} must have an orb spawner to work properly.", this);
    }

    public void StateEnter() 
    {
        _player.DisableMovement = false;
        _candle.CandleStateFreeze = false;
    }

    public void StateUpdate() 
    {
        // Monitor game over request
        if (Data.ShouldBeGameOver){
            _stateMachine.ChangeState(_gameOverState);
            return;
        }

        if (_player.IsPaused) { _pauseState.Data.ShouldBePaused = _player.IsPaused; }

        // Monitor game pause request
        if(_pauseState.Data.ShouldBePaused) {
            _stateMachine.ChangeState(_pauseState);
            return;
        }

        if(_player.IsCandleOut) {
            _orbSpawner.transform.position = _player.transform.position;
            _orbSpawner.TrySpawnOrbsWithCooldown();
        }
    }

    public void StateFixedUpdate() {}

    public void StateExit() 
    {
        _player.DisableMovement = true;
        _candle.CandleStateFreeze = true;
    }
}
