using UnityEngine;
using UnityEngine.Pool;

public class SpiritOrbSpawner : MonoBehaviour
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

    public SpiritOrb GetOrbFromPool() {
        return _orbPool.Get();
    }

    public void ReturnOrbToPool(SpiritOrb orb) {
        orb.OnDespawn -= ReturnOrbToPool;
        _orbPool.Release(orb);
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
