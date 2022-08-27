using UnityEngine;

public class SpiritOrbSpawner : MonoBehaviour
{
    [SerializeField]
    private SpiritOrb _orbPrefab;

    [SerializeField, Min(1f)]
    private float _spawnRadius;

    public void SpawnOrb() {
        SpiritOrb orb = GetOrbFromPool();
        orb.OnDespawn += ReturnOrbToPool;

        Vector2 startPosition = Random.insideUnitCircle * _spawnRadius;
        Vector2 targetPosition = Random.insideUnitCircle * _spawnRadius;

        orb.Spawn(startPosition, targetPosition);
    }

    public SpiritOrb GetOrbFromPool() {
        // TODO: actually get from pool
        return Instantiate(_orbPrefab);
    }

    public void ReturnOrbToPool(SpiritOrb orb) {
        // TODO: actually return it to the pool
        orb.OnDespawn -= ReturnOrbToPool;
        Destroy(orb.gameObject);
    }

#if UNITY_EDITOR
    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _spawnRadius);
    }
#endif
}
