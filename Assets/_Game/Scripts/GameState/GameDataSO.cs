using UnityEngine;

[CreateAssetMenu(menuName = "Game Jam/Game Data")]
public class GameDataSO : ScriptableObject
{
    [SerializeField]
    private GameData _gameData;

    public GameData Data { get => _gameData; }

    private void Reset() {
        _gameData = new();
    }
    public bool LoadPlayerData()
    {
        PlayerController player = FindObjectOfType<PlayerController>();
        if (player == null)
        {
            Debug.LogWarning("Load player position failed! No player controllers could be found in this scene.");
            return false;
        }

        player.transform.position = _gameData.PlayerPosition;
        return true;
    }

    public bool LoadPuzzleData() {
        ColorPuzzleController[] puzzleControllers = FindObjectsOfType<ColorPuzzleController>();
        if(puzzleControllers.Length == 0) 
        {
            Debug.LogWarning("Load puzzle data failed! No puzzle controllers could be found in this scene.");
            return false;
        }

        for (int j = 0; j < puzzleControllers.Length; j++) 
        {
            ColorPuzzleController controller = puzzleControllers[j];
            PuzzleData puzzleData = _gameData.Puzzles.Find(x => x.PuzzleId == controller.Id);
            if(puzzleData == null) 
            {
                Debug.LogWarning($"PuzzleController[{controller.Id}] save data not found! Skipping...", controller);
                continue;
            }
            if(puzzleData.IsPuzzleComplete) {
                for(var i = 0; i < puzzleData.Switches.Count; i++) 
                {
                    ColorPuzzleSwitch puzzleSwitch = controller.Switches.Find(x => x.SwitchName == puzzleData.Switches[i]);
                    if(puzzleSwitch == null) continue;
                    controller.ActivatedSwitches.Add(puzzleSwitch);
                }
                controller.Activate(puzzleData.PuzzleId);
            }
        }

        return true;
    }

    public bool LoadCheckPointData()
    {
        CheckpointTrigger[] checkpoints = FindObjectsOfType<CheckpointTrigger>();
        if (checkpoints.Length == 0)
        {
            Debug.LogWarning("Load CheckPoint data failed! No CheckpointTrigger could be found in this scene.");
            return false;
        }

        for (int i = 0; i < checkpoints.Length; i++)
        {
            CheckPointData checkPointData = _gameData.Checkpoints.Find(x => x.CheckPointId == checkpoints[i].Id);
            if(checkPointData == null)
            {
                Debug.LogWarning($"CheckPointTrigger[{checkpoints[i].Id}] save data not found! Skipping...", checkpoints[i]);
                continue;
            }
            else
            {
                if (checkPointData.IsCheckPointActivated) { checkpoints[i].Activate(); }
                else { checkpoints[i].Deactivate(); }
            }

        }
        return true;
    }

    public void SavePlayerData()
    {
        var player = FindObjectOfType<PlayerController>();
        if (player != null)
        {
            _gameData.PlayerPosition = player.transform.position;
        }
        else
        {
            Debug.LogWarning("Save player Data failed! No player controllers could be found in this scene.");
        }
    }

    public void SavePuzzleData() {
        ColorPuzzleController[] colorPuzzleControllers = FindObjectsOfType<ColorPuzzleController>(true);

        for(int j = 0; j < colorPuzzleControllers.Length; j++) 
        {
            ColorPuzzleController colorPuzzleController = colorPuzzleControllers[j];
            PuzzleData puzzleData = _gameData.Puzzles.Find(x => x.PuzzleId == colorPuzzleController.Id);
            if(puzzleData == null) 
            {
                //Add new entry
                puzzleData = new PuzzleData(colorPuzzleController.Id, colorPuzzleController.IsPuzzleComplete);
                for(var i = 0; i < colorPuzzleController.ActivatedSwitches.Count; i++) 
                {
                    puzzleData.Switches.Add(colorPuzzleController.ActivatedSwitches[i].SwitchName);
                }
                _gameData.Puzzles.Add(puzzleData);
            } 
            else 
            {
                //Overwrite entry
                puzzleData.Switches.Clear();
                puzzleData.IsPuzzleComplete = colorPuzzleController.IsPuzzleComplete;
                for(var i = 0; i < colorPuzzleController.ActivatedSwitches.Count; i++) 
                {
                    puzzleData.Switches.Add(colorPuzzleController.ActivatedSwitches[i].SwitchName);
                }
            }
        }
    }

    public void SaveCheckPointData()
    {
        CheckpointTrigger[] checkpoints = FindObjectsOfType<CheckpointTrigger>();

        for (int i = 0; i < checkpoints.Length; i++)
        {
            CheckPointData checkPointData = _gameData.Checkpoints.Find(x => x.CheckPointId == checkpoints[i].Id);
            if(checkPointData == null)
            {
                _gameData.Checkpoints.Add(new CheckPointData(checkpoints[i].Id, checkpoints[i].IsActivated));
            }
            else
            {
                checkPointData.IsCheckPointActivated = checkpoints[i].IsActivated;
            }
        }

    }

    public void SaveGameData()
    {
        SavePlayerData();
        SavePuzzleData();
        SaveCheckPointData();
    }

    public void LoadGameData()
    {
        LoadPlayerData();
        LoadPuzzleData();
        LoadCheckPointData();
    }
}
