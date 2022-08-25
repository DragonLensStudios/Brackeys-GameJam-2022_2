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

    public PuzzleData() {
        _puzzleId = "";
        _isPuzzleComplete = false;
    }

    public PuzzleData(string puzzleId, bool isPuzzleComplete) {
        _puzzleId = puzzleId;
        IsPuzzleComplete = isPuzzleComplete;
    }
}
