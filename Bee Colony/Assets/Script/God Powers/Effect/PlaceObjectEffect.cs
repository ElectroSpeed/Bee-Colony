using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "GodGame/Effects/PlaceObjectEffect")]
public class PlaceObjectEffect : EffectBehaviour
{
    [SerializeField] private GameObject _prefabToPlace;

    public override void ApplyEffect(IEnumerable<ITarget> targets)
    {
        if (_prefabToPlace == null)
        {
            return;
        }

        if (targets == null)
        {
            return;
        }

        foreach (var target in targets)
        {
            if (target == null) continue;

            Vector3 pos = target.GetTransform() != null
                ? target.GetTransform().position
                : (target is GenericTarget gt ? gt._position : Vector3.zero);

            GameObject placed = Instantiate(_prefabToPlace, pos, Quaternion.identity);
            
            GameObject targetEntity = target.GetEntity();
            if (targetEntity != null && targetEntity.name.Contains("RaycastTarget"))
            {
                Object.Destroy(targetEntity);
            }
        }
    }
}