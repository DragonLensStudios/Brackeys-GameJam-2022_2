using System;
using UnityEngine;

public class SpiritOrb : MonoBehaviour
{
    [SerializeField] private AnimationCurve _lerpCurve = new(new(0, 0), new(1, 1));
    [SerializeField] private float _speed = 0.5f;

    private Vector2 _startPosition;
    private Vector2 _targetPosition;

    private float _currentLerp;
    private readonly float _targetLerp = 1f;

    public event Action<SpiritOrb> OnSpawn;
    public event Action<SpiritOrb> OnDespawn;

    private void Update() {
        if(_currentLerp == _targetLerp) {
            Despawn();
        }

        Move();
    }

    private void Move() {
        _currentLerp = Mathf.MoveTowards(_currentLerp, _targetLerp, _speed * Time.deltaTime);

        transform.position = Vector2.Lerp(_startPosition, _targetPosition, _lerpCurve.Evaluate(_currentLerp));
    }

    public void Spawn(Vector2 startPosition, Vector2 targetPosition) {
        _startPosition = startPosition;
        _targetPosition = targetPosition;

        transform.position = _startPosition;
        _currentLerp = 0f;

        OnSpawn?.Invoke(this);
    }

    private void Despawn() {
        OnDespawn?.Invoke(this);
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.CompareTag("Player")) {
            Despawn();
            // TODO: trigger game over?
        }
    }
}
