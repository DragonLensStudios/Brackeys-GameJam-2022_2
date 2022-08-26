using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Holds information about a specific checkpoint to be loaded at runtime.
/// Specifically to work with checkpoints to save/load data.
/// </summary>
[System.Serializable]
public class CheckPointData
{
    [SerializeField, Tooltip("The string ID of the checkpoint.")]
    private string _checkPointId;

    [SerializeField, Tooltip("The actiavated status of the checkpoint.")]
    private bool _isCheckpointActivated;

    /// <summary>
    /// The string ID of the Checkpoint.
    /// </summary>
    public string CheckPointId { get => _checkPointId; }

    /// <summary>
    /// The completion status of the puzzle.
    /// </summary>
    public bool IsCheckPointActivated
    {
        get => _isCheckpointActivated;
        set => _isCheckpointActivated = value;
    }

    public CheckPointData()
    {
        _checkPointId = "";
        _isCheckpointActivated = false;
    }

    public CheckPointData(string checkPointId, bool isCheckpointActivated)
    {
        _checkPointId = checkPointId;
        _isCheckpointActivated = isCheckpointActivated;
    }
}