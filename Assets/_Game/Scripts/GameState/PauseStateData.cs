using UnityEngine;

//[CreateAssetMenu(menuName = "Game Jam/Pause State Data")]
public class PauseStateData : ScriptableObject
{
    [SerializeField]
    private bool shouldBePaused;
    public bool ShouldBePaused { get => shouldBePaused; set => shouldBePaused = value; }

    private void OnEnable() {
        shouldBePaused = false;
    }
}

