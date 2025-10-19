using UnityEngine;

public abstract class BeeState
{
    protected Bee _bee;
    protected BeeStateMachine _stateMachine;

    protected BeeState(Bee bee, BeeStateMachine stateMachine)
    {
        _bee = bee;
        _stateMachine = stateMachine;
    }

    public virtual void Enter() { }
    public virtual void Update() { }
    public virtual void Exit() { }
}