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

    public GameData() {
        _puzzles = new();
        _playerPosition = Vector3.zero;
    }

    public GameData(List<PuzzleData> puzzles, Vector3 position) {
        _puzzles = puzzles;
        _playerPosition = position;
    }
}
