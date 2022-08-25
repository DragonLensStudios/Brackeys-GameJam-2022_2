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

    public bool LoadPuzzleData() {
        ColorPuzzleController[] puzzleControllers = FindObjectsOfType<ColorPuzzleController>();
        if(puzzleControllers.Length == 0) {
            Debug.LogWarning("Load puzzle data failed! No puzzle controllers could be found in this scene.");
            return false;
        }

        foreach(var controller in puzzleControllers) {
            PuzzleData puzzleData = _gameData.Puzzles.Find(x => x.PuzzleId == controller.Id);
            if(puzzleData == null) {
                Debug.LogWarning($"PuzzleController[{controller.Id}] save data not found! Skipping...", controller);
                continue;
            }
            if(puzzleData.IsPuzzleComplete) {
                for(var i = 0; i < puzzleData.Switches.Count; i++) {
                    ColorPuzzleSwitch puzzleSwitch = controller.Switches.Find(x => x.SwitchName == puzzleData.Switches[i]);
                    if(puzzleSwitch == null) continue;
                    controller.ActivatedSwitches.Add(puzzleSwitch);
                }
                controller.Activate();
            }
        }

        return true;
    }

    public bool LoadPlayerPosition() {
        PlayerController player = FindObjectOfType<PlayerController>();
        if(player == null) {
            Debug.LogWarning("Load player position failed! No player controllers could be found in this scene.");
            return false;
        }

        player.transform.position = _gameData.PlayerPosition;
        return true;
    }

    public void SavePuzzleData() {
        ColorPuzzleController[] colorPuzzleControllers = FindObjectsOfType<ColorPuzzleController>(true);

        for(int j = 0; j < colorPuzzleControllers.Length; j++) {
            ColorPuzzleController colorPuzzleController = colorPuzzleControllers[j];
            PuzzleData puzzleData = _gameData.Puzzles.Find(x => x.PuzzleId == colorPuzzleController.Id);
            if(puzzleData == null) {
                //Add new entry
                _gameData.Puzzles.Add(new PuzzleData(
                    colorPuzzleController.Id,
                    colorPuzzleController.IsPuzzleComplete
                    ));
                for(var i = 0; i < colorPuzzleController.ActivatedSwitches.Count; i++) {
                    puzzleData.Switches.Add(colorPuzzleController.ActivatedSwitches[i].SwitchName);
                }
            } else {
                //Overwrite entry
                puzzleData.Switches.Clear();
                puzzleData.IsPuzzleComplete = colorPuzzleController.IsPuzzleComplete;
                for(var i = 0; i < colorPuzzleController.ActivatedSwitches.Count; i++) {
                    puzzleData.Switches.Add(colorPuzzleController.ActivatedSwitches[i].SwitchName);
                }
            }

            _gameData.PlayerPosition = colorPuzzleController.transform.position;
        }
    }
}
