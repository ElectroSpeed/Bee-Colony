using UnityEngine;

public class BeeIdleState : BeeState
{
    private float _idleTimer;

    public BeeIdleState(Bee bee, BeeStateMachine stateMachine) : base(bee, stateMachine) { }

    public override void Enter()
    {
        _idleTimer = Random.Range(1f, 3f);
    }

    public override void Update()
    {
        _idleTimer -= Time.deltaTime;
        if (_idleTimer <= 0f)
        {
            _stateMachine.ChangeState(new BeeForageState(_bee, _stateMachine));
        }
    }
}