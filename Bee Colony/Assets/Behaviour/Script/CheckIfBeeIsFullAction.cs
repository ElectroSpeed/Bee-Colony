using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "Check If Bee is full", story: "Check if a [bee] is full", category: "Action", id: "50ec02e82bfc2185072f1fef8031da82")]
public partial class CheckIfBeeIsFullAction : Action
{
    [SerializeReference] public BlackboardVariable<Bee> Bee;
    [SerializeReference] public BlackboardVariable<float> maxCapacity;

    protected override Status OnStart()
    {
        
        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        return Status.Success;
    }

    protected override void OnEnd()
    {
    }
}

