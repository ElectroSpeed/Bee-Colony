using System.Collections.Generic;
using UnityEngine;

public abstract class TargetingBehaviour : MonoBehaviour, ITargetingStrategy
{
    public abstract IEnumerable<ITarget> GetTargets(Vector3 origin);
}