using System;
using UnityEngine;

public class SpiritOrb : MonoBehaviour, IPauseable
{
    [SerializeField]
    private AnimationCurve _accelerationCurve = new(new(0, 0), new(1, 1));
    [SerializeField]
    private float _accelerationDuration = 5f;

    [Header("Speed")]
    [SerializeField]
    private float _startSpeed = 0.1f;
    [SerializeField]
    private float _targetSpeed = 1f;

    [Header("Duration")]
    [SerializeField] private float _lifetime = 10f;

    [Header("Misc")]
    [SerializeField] private PlayingStateData _stateData;

    private Vector2 _startPosition;
    private Vector2 _targetPosition;
    private Vector2 _direction;
    private float _currentSpeed;

    private float _currentLerp;
    private readonly float _targetLerp = 1f;
    private bool _isPaused = false;
    private float _currentLifetime;

    public event Action<SpiritOrb> OnSpawn;
    public event Action<SpiritOrb> OnDespawn;

    private void Update() {
        if(_isPaused) return;

        if(_currentLifetime <= 0f) {
            Despawn();
            return;
        }

        Accelerate();
        Move();

        _currentLifetime -= Time.deltaTime;
    }

    private void Accelerate() {
        if(_currentLerp == _targetLerp) return;

        _currentLerp = Mathf.MoveTowards(_currentLerp, _targetLerp, (1 / _accelerationDuration) * Time.deltaTime);

        _currentSpeed = Mathf.Lerp(_startSpeed, _targetSpeed, _accelerationCurve.Evaluate(_currentLerp));
    }

    private void Move() {
        Vector2 currentPosition = transform.position;
        transform.position = currentPosition + _direction * _currentSpeed * Time.deltaTime;
    }

    public void Spawn(Vector2 startPosition, Vector2 targetPosition) {
        _startPosition = startPosition;
        _targetPosition = targetPosition;

        transform.position = _startPosition;
        _currentLerp = 0f;
        _currentLifetime = _lifetime;

        _direction = (_targetPosition - _startPosition).normalized;
        _currentSpeed = _startSpeed;

        OnSpawn?.Invoke(this);
    }

    private void Despawn() {
        OnDespawn?.Invoke(this);
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.CompareTag("Player")) {
            Despawn();
            _stateData.ShouldBeGameOver = true;
        }
    }

    public void OnGamePaused() {
        _isPaused = true;
    }

    public void OnGameUnpaused() {
        _isPaused = false;
    }
}
