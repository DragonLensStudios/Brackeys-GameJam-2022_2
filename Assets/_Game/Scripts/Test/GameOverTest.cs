using UnityEngine;
public class GameOverTest : MonoBehaviour
{
    [SerializeField] private GameOverStateData _gameOverData;
    [SerializeField] private GameObject[] _disabledOnStart;
    [SerializeField] private GameObject[] _toEnable;
    [SerializeField] private GameObject[] _toDisable;

    private void Start() {
        foreach(GameObject go in _disabledOnStart) {
            go.SetActive(false);
        }
    }

    private void OnEnable() {
        _gameOverData.OnGameOver += OnGameOver;
    }

    private void OnDisable() {
        _gameOverData.OnGameOver -= OnGameOver;
    }

    private void OnGameOver() {
        foreach(GameObject go in _toDisable) {
            go.SetActive(false);
        }
        foreach(GameObject go in _toEnable) {
            go.SetActive(true);
        }
    }
}

