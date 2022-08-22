using UnityEngine;

namespace DragonLens.BrackeysGameJam2022_2.GameStates
{
    //[CreateAssetMenu(menuName = "Game Jam/Pause State Data")]
    public class PauseStateData : ScriptableObject
    {
        public bool ShouldBePaused { get; set; }

        private void OnEnable() {
            ShouldBePaused = false;
        }
    }
}
