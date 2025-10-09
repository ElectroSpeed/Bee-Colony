using System.Collections.Generic;
using UnityEngine;

public abstract class PowerBase : MonoBehaviour, IPower
{
    [SerializeField] protected PowerData _data;
    protected float _lastActivationTime;

    public virtual bool CanActivatePower()
    {
        return Time.time >= _lastActivationTime + _data.Cooldown;
    }

    public virtual void ActivatePower()
    {
        if (!CanActivatePower()) return;

        _lastActivationTime = Time.time;

        if (_data.TargetingPrefab == null || _data.EffectPrefabs == null) return;

        IEnumerable<ITarget> targets = _data.TargetingPrefab.GetTargets(transform.position);

        foreach (var effect in _data.EffectPrefabs)
        {
            if (effect != null)
                effect.ApplyEffect(targets);
        }
    }

}