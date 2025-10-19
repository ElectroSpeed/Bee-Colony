using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "GodGame/Effects/DestroyTargetsEffect")]
public class DestroyTargetsEffect : EffectBehaviour
{
    public override void ApplyEffect(IEnumerable<ITarget> targets)
    {
        if (targets == null) return;

        foreach (var target in targets)
        {
            GameObject entity = target.GetEntity();

            if (entity != null)
            {
                Object.Destroy(entity);
            }
        }
    }
}