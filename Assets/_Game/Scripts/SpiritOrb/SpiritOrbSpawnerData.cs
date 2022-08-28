using UnityEngine;

[CreateAssetMenu(menuName = "Game Jam/Orb Spawner Settings")]
public class SpiritOrbSpawnerData : ScriptableObject
{
    [SerializeField]
    private int _amountToSpawn = 1;
    [SerializeField]
    private float _cooldownBetweenSpawns = 5f;
    [SerializeField, Min(1f), Tooltip("Spawn within this circle.")]
    private float _spawnRadius;
    [SerializeField, Min(0f), Tooltip("DO NOT spawn within this circle.")]
    private float _deadzoneRadius;
    [SerializeField, Min(1f), Tooltip("Target a position within this circle to move towards.")]
    private float _targetRadius;

    public int AmountToSpawn => _amountToSpawn;
    public float CooldownBetweenSpawns => _cooldownBetweenSpawns;
    public float SpawnRadius => _spawnRadius;
    public float DeadzoneRadius => _deadzoneRadius;
    public float TargetRadius => _targetRadius;
}
