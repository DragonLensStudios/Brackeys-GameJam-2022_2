using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Holds information about a specific puzzle controller to be loaded at runtime.
/// Specifically to work with checkpoints to save/load data.
/// </summary>
[System.Serializable]
public class PuzzleData
{
    [SerializeField, Tooltip("The string ID of the puzzle controller.")]
    private string _puzzleId;

    [SerializeField, Tooltip("The completion status of the puzzle.")]
    private bool _isPuzzleComplete;

    [SerializeField]
    private List<string> _switches;

    /// <summary>
    /// The string ID of the puzzle controller.
    /// </summary>
    public string PuzzleId { get => _puzzleId; }

    /// <summary>
    /// The completion status of the puzzle.
    /// </summary>
    public bool IsPuzzleComplete {
        get => _isPuzzleComplete;
        set => _isPuzzleComplete = value;
    }
    public List<string> Switches { get => _switches; set => _switches = value; }

    public PuzzleData() {
        _puzzleId = "";
        _isPuzzleComplete = false;
        _switches = new();
    }

    public PuzzleData(string puzzleId, bool isPuzzleComplete) {
        _puzzleId = puzzleId;
        IsPuzzleComplete = isPuzzleComplete;
        _switches = new();
    }
}
