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
            controller.IsPuzzleComplete = puzzleData.IsPuzzleComplete;
        }

        return true;
    }
}
