using UnityEngine;

public class GenericTarget : ITarget
{
    public Vector3 _position { get; private set; }
    public GameObject _entity { get; private set; }

    public GenericTarget(GameObject obj)
    {
        _entity = obj;
        _position = obj.transform.position;
    }
}