using UnityEngine;

//[CreateAssetMenu(menuName = "Game Jam/Playing State Data")]
public class PlayingStateData : ScriptableObject
{
    [SerializeField]
    private bool _shouldBeGameOver;
    public bool ShouldBeGameOver { get => _shouldBeGameOver; set => _shouldBeGameOver = value; }

    private void OnEnable() {
        _shouldBeGameOver = false;
    }
}
