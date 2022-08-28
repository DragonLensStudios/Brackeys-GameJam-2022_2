using UnityEngine;
using UnityEngine.Pool;

public class SpiritOrbSpawner : MonoBehaviour, IPauseable
{
    [SerializeField]
    private SpiritOrb _orbPrefab;
    [SerializeField]
    private SpiritOrbSpawnerData _spawnerData;
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
        }, false, _spawnerData.AmountToSpawn, _maxCapacity);
    }

    /// <summary>
    /// Spawns a single orb with a random start/target position within the spawn radius.
    /// </summary>
    public void SpawnOrb() {
        SpiritOrb orb = GetOrbFromPool();
        orb.OnDespawn += ReturnOrbToPool;

        float randomRadius = Random.Range(_spawnerData.DeadzoneRadius, _spawnerData.SpawnRadius);
        float randomAngleRads = Random.Range(0f, Mathf.PI * 2);
        float startX = Mathf.Cos(randomAngleRads) * randomRadius;
        float startY = Mathf.Sin(randomAngleRads) * randomRadius;
        Vector2 startPosition = new Vector2(startX, startY);
        Vector2 targetPosition = Random.insideUnitCircle * _spawnerData.TargetRadius;

        orb.Spawn(startPosition, targetPosition);
    }

    /// <summary>
    /// Uses a cooldown timer to decide if it should spawn orbs or not.
    /// </summary>
    public void TrySpawnOrbsWithCooldown() {
        if(_currentCooldown > 0f) return;

        for(var i = 0; i < _spawnerData.AmountToSpawn; i++) {
            SpawnOrb();
        }

        _currentCooldown = _spawnerData.CooldownBetweenSpawns;
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
    [Header("Editor Only")]
    [SerializeField]
    private bool _drawGizmos = true;

    private void OnDrawGizmos() {
        if(_spawnerData == null || !_drawGizmos) return;

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, _spawnerData.SpawnRadius);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _spawnerData.DeadzoneRadius);

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, _spawnerData.TargetRadius);
    }
#endif
}
