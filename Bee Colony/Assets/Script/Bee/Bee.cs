using UnityEngine;

public class Bee : MonoBehaviour
{
    [SerializeField] private float _carryCapacity = 5f;
    [SerializeField] private float _speed = 5f;
    [SerializeField] private float _fatigueRate = 5f;
    [SerializeField] private float _recoveryAmount = 20f;

    private IPollenReceiver _receiver;
    private int _carriedPollen;
    private float _fatigue;

    private bool _isOnExpedition;

    public void Init(IPollenReceiver receiver)
    {
        _receiver = receiver;
    }

    private void Update()
    {
        if (_isOnExpedition)
        {
            _fatigue += _fatigueRate * Time.deltaTime;
            _fatigue = Mathf.Clamp(_fatigue, 0, 100);
        }
    }

    public bool IsCarryingPollen()
    {
        return _carriedPollen > 0;
    }

    public bool IsTired()
    {
        return _fatigue >= 100;
    }

    public void StartExpedition()
    {
        _isOnExpedition = true;
    }

    public void EndExpedition()
    {
        _isOnExpedition = false;
    }

    public void CollectPollen(Flower flower)
    {
        if (flower.ContainsPollen())
        {
            int pollen = flower.GetPollen();
            int amountTaken = Mathf.Min((int)_carryCapacity, pollen);
            _carriedPollen = amountTaken;
        }
    }

    public void DepositPollen()
    {
        if (_receiver != null && _carriedPollen > 0)
        {
            _receiver.AddPollen(_carriedPollen);
            _carriedPollen = 0;
            Recover();
        }
    }

    private void Recover()
    {
        _fatigue -= _recoveryAmount;
        _fatigue = Mathf.Clamp(_fatigue, 0, 100);
    }

    public float GetFatigue() => _fatigue;
}