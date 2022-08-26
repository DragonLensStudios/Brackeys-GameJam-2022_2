using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Holds information for the game to save/load from.
/// </summary>
[System.Serializable]
public class GameData
{
    [SerializeField, Tooltip("World space position to place the player on load.")]
    private Vector3 _playerPosition;

    [SerializeField, Tooltip("The list of puzzles and their associated data.")]
    private List<PuzzleData> _puzzles;

    [SerializeField, Tooltip("The list of checkpoints and their associated data.")]
    private List<CheckPointData> _checkpoints;
    /// <summary>
    /// World space position to place the player on load.
    /// </summary>
    public Vector3 PlayerPosition {
        get => _playerPosition;
        set => _playerPosition = value;
    }

    /// <summary>
    /// The list of puzzles and their associated data.
    /// </summary>
    public List<PuzzleData> Puzzles { get => _puzzles; }

    public List<CheckPointData> Checkpoints { get => _checkpoints; }

    public GameData() {
        _puzzles = new();
        _playerPosition = Vector3.zero;
    }

    public GameData(List<PuzzleData> puzzles, List<CheckPointData> checkpoints, Vector3 position) {
        _puzzles = puzzles;
        _checkpoints = checkpoints;
        _playerPosition = position;
    }
}
