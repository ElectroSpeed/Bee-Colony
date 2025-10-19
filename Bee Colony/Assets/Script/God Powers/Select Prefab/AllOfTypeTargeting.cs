using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "GodGame/Targeting/AllOfTypeTargeting")]
public class AllOfTypeTargeting : TargetingBehaviour
{
    [SerializeField] private string _tagFilter;
    [SerializeField] private bool _useTagFilter = false;

    public override IEnumerable<ITarget> GetTargets(Vector3 origin)
    {
        List<ITarget> targets = new List<ITarget>();
        
        Bee[] bees = GameObject.FindObjectsOfType<Bee>();

        foreach (var bee in bees)
        {
            if (bee == null) continue;
            
            if (_useTagFilter && !bee.CompareTag(_tagFilter))
                continue;

            targets.Add(new GenericTarget(bee.gameObject));
        }

        return targets;
    }
}