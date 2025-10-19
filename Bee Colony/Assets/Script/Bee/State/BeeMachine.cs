using UnityEngine;

public class BeeStateMachine
{
    private BeeState _currentState;

    public void Initialize(BeeState startState)
    {
        _currentState = startState;
        _currentState.Enter();
    }

    public void ChangeState(BeeState newState)
    {
        _currentState.Exit();
        _currentState = newState;
        _currentState.Enter();
    }

    public void Update()
    {
        _currentState?.Update();
    }
}