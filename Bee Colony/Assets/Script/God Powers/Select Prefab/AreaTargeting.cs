using System.Collections.Generic;
using UnityEngine;

public class AreaTargeting : TargetingBehaviour
{
    [SerializeField] private float _radius = 5f;
    [SerializeField] private LayerMask _targetLayer;

    public override IEnumerable<ITarget> GetTargets(Vector3 origin)
    {
        Collider[] hits = Physics.OverlapSphere(origin, _radius, _targetLayer);

        foreach (var hit in hits)
        {
            yield return new GenericTarget(hit.gameObject);
        }
    }

#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _radius);
    }
#endif
}
