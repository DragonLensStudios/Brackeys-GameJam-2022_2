using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Holds information for the game to save/load from.
/// </summary>
[System.Serializable]
public class GameData
{
    [SerializeField, Tooltip("The list of puzzles and their associated data.")]
    private List<PuzzleData> _puzzles;

    /// <summary>
    /// The list of puzzles and their associated data.
    /// </summary>
    public List<PuzzleData> Puzzles { get => _puzzles; }

    public GameData() {
        _puzzles = new();
    }

    public GameData(List<PuzzleData> puzzles) {
        _puzzles = puzzles;
    }
}
