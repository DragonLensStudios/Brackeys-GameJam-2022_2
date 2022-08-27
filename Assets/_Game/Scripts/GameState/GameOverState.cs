using UnityEngine;

public class GameOverState : MonoBehaviour, IState
{
    public GameOverStateData Data;

    public void StateEnter() 
    {
        Data.NotifyGameOverListeners();
        print("Game Over");
    }
    public void StateUpdate() {}
    public void StateFixedUpdate() {}
    public void StateExit() {}
}
