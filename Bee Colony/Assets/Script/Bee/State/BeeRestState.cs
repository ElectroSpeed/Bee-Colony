using UnityEngine;

public class BeeRestState : BeeState
{
    private float _restTimer = 3f;

    public BeeRestState(Bee bee, BeeStateMachine stateMachine) : base(bee, stateMachine) { }

    public override void Enter()
    {
        _bee.EndExpedition();
    }

    public override void Update()
    {
        _restTimer -= Time.deltaTime;
        if (_restTimer <= 0f)
        {
            _stateMachine.ChangeState(new BeeIdleState(_bee, _stateMachine));
        }
    }
}