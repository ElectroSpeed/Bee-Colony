using System.Collections.Generic;
using UnityEngine;

public abstract class EffectBehaviour : ScriptableObject, IEffect
{
    public abstract void ApplyEffect(IEnumerable<ITarget> targets);
}