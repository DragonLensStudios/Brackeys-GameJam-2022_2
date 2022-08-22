using DragonLens.BrackeysGameJam2022_2.GameStates;
using UnityEngine;

namespace DragonLens.BrackeysGameJam2022_2.Tests
{
    public class PauseableTest : MonoBehaviour, IPauseable
    {
        public void OnGamePaused() {
            print($"{gameObject.name}: paused");
        }

        public void OnGameUnpaused() {
            print($"{gameObject.name}: unpaused");
        }
    }
}
