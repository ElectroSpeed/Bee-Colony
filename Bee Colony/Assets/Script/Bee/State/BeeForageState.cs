using UnityEngine;
using System.Linq;

public class BeeForageState : BeeState
{
    private Flower _targetFlower;

    public BeeForageState(Bee bee, BeeStateMachine stateMachine) : base(bee, stateMachine) { }

    public override void Enter()
    {
        _bee.StartExpedition();
        
        Flower[] flowers = GameObject.FindObjectsOfType<Flower>();
        if (flowers.Length > 0)
        {
            _targetFlower = flowers[Random.Range(0, flowers.Length)];
        }
    }

    public override void Update()
    {
        if (_targetFlower == null)
        {
            _stateMachine.ChangeState(new BeeIdleState(_bee, _stateMachine));
            return;
        }

        _bee.MoveTowards(_targetFlower.transform.position);

        if (_bee.ReachedDestination())
        {
            _bee.CollectPollen(_targetFlower);
            _stateMachine.ChangeState(new BeeReturnState(_bee, _stateMachine));
        }
    }


    public override void Exit()
    {
        _bee.EndExpedition();
    }
}