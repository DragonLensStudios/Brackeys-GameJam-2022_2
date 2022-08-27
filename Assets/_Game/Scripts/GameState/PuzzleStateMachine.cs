using UnityEngine;

public class PuzzleStateMachine : StateMachine
{
    [SerializeField] private InitializationState _initializationState;

    private void Start() 
    {
        ChangeState(_initializationState);
    }
}
