using UnityEngine;

public class BeeReturnState : BeeState
{
    private Beehive _hive;

    public BeeReturnState(Bee bee, BeeStateMachine stateMachine) : base(bee, stateMachine) { }

    public override void Enter()
    {
        _hive = GameObject.FindObjectOfType<Beehive>();
    }

    public override void Update()
    {
        if (_hive == null)
        {
            _stateMachine.ChangeState(new BeeIdleState(_bee, _stateMachine));
            return;
        }

        _bee.MoveTowards(_hive.transform.position);

        if (_bee.ReachedDestination())
        {
            _bee.DepositPollen();

            if (_bee.IsTired())
                _stateMachine.ChangeState(new BeeRestState(_bee, _stateMachine));
            else
                _stateMachine.ChangeState(new BeeIdleState(_bee, _stateMachine));
        }
    }

}