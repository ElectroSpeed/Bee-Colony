using System.Collections.Generic;
using UnityEngine;

public abstract class EffectBehaviour : MonoBehaviour, IEffect
{
    public abstract void ApplyEffect(IEnumerable<ITarget> targets);
}