using UnityEngine;

public abstract class StateMachine : MonoBehaviour
{
	public IState CurrentState { get; private set; }

	private bool _inTransition = false;

	public void ChangeState(IState newState) 
	{
		if(CurrentState == newState || _inTransition) return;

        _inTransition = true;

        if(CurrentState != null) CurrentState.StateExit();

        CurrentState = newState;

        if(CurrentState != null) CurrentState.StateEnter();

        _inTransition = false;
    }

	private void Update() 
	{
		if(CurrentState == null) return;
		if(_inTransition) return;

		CurrentState.StateUpdate();
	}

	private void FixedUpdate() 
	{
        if(CurrentState == null) return;
        if(_inTransition) return;

		CurrentState.StateFixedUpdate();
	}
}
