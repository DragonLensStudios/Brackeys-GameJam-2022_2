using UnityEngine;

public class PauseableTest : MonoBehaviour, IPauseable
{
    public void OnGamePaused() {
        print($"{gameObject.name}: paused");
    }

    public void OnGameUnpaused() {
        print($"{gameObject.name}: unpaused");
    }
}
