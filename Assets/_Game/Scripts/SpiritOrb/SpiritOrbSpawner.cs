using UnityEngine;
using UnityEngine.Pool;

public class SpiritOrbSpawner : MonoBehaviour, IPauseable
{
    [SerializeField]
    private SpiritOrb _orbPrefab;

    [Header("Spawn Settings")]
    [SerializeField, Min(1f), Tooltip("Spawn within this circle.")]
    private float _spawnRadius;
    [SerializeField, Min(0f), Tooltip("DO NOT spawn within this circle.")]
    private float _deadzoneRadius;

    [Header("Object Pool")]
    [SerializeField, Tooltip("Starting pool size.")]
    private int _minCapacity = 10;
    [SerializeField, Tooltip("Max pool size.")]
    private int _maxCapacity = 100;

    private ObjectPool<SpiritOrb> _orbPool;
    private float _currentCooldown = 0f;
    private bool _isPaused = false;

    private void Awake() {
        SetupObjectPool();
    }

    private void SetupObjectPool() {
        _orbPool = new(() => {
            return Instantiate(_orbPrefab, transform);
        }, orb => {
            orb.gameObject.SetActive(true);
        }, orb => {
            orb.gameObject.SetActive(false);
        }, orb => {
            Destroy(orb.gameObject);
        }, false, _minCapacity, _maxCapacity);
    }

    /// <summary>
    /// Spawns a single orb with a random start/target position within the spawn radius.
    /// </summary>
    public void SpawnOrb() {
        SpiritOrb orb = GetOrbFromPool();
        orb.OnDespawn += ReturnOrbToPool;

        Vector2 startPosition = Random.insideUnitCircle * _spawnRadius;
        if(startPosition.magnitude < _deadzoneRadius) {
            float scalar = _deadzoneRadius / (_deadzoneRadius - startPosition.magnitude);
            startPosition *= scalar;
        }
        Vector2 targetPosition = Random.insideUnitCircle * _spawnRadius;

        orb.Spawn(startPosition, targetPosition);
    }

    /// <summary>
    /// Uses a cooldown timer to decide if it should spawn orbs or not.
    /// </summary>
    /// <param name="spawnAmount">The amount you want to spawn.</param>
    /// <param name="cooldown">Seconds. How long should the cooldown be to spawn the next wave?</param>
    public void TrySpawnOrbsWithCooldown(int spawnAmount, float cooldown) {
        if(_currentCooldown > 0f) return;

        for(var i = 0; i < spawnAmount; i++) {
            SpawnOrb();
        }

        _currentCooldown = cooldown;
    }

    private void Update() {
        if(_isPaused) return;
        if(_currentCooldown <= 0f) return;

        _currentCooldown -= Time.deltaTime;
    }

    private SpiritOrb GetOrbFromPool() {
        return _orbPool.Get();
    }

    private void ReturnOrbToPool(SpiritOrb orb) {
        orb.OnDespawn -= ReturnOrbToPool;
        _orbPool.Release(orb);
    }
    public void OnGamePaused() {
        _isPaused = true;
    }

    public void OnGameUnpaused() {
        _isPaused = false;
    }

#if UNITY_EDITOR
    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, _spawnRadius);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _deadzoneRadius);
    }
#endif
}
