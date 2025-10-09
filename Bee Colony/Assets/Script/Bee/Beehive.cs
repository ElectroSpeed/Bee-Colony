using UnityEngine;

public class Beehive : MonoBehaviour, IPollenReceiver
{
    [SerializeField] private int _pollenStock = 0;
    [SerializeField] private int _honeyStock = 0;

    [SerializeField] private int _pollenToHoneyRate = 5;
    [SerializeField] private int _honeyToBeeRate = 3;

    [SerializeField] private GameObject _beePrefab;
    [SerializeField] private Transform _spawnPoint;

    public void AddPollen(int amount)
    {
        _pollenStock += amount;
        TryProduceHoney();
    }

    private void TryProduceHoney()
    {
        while (_pollenStock >= _pollenToHoneyRate)
        {
            _pollenStock -= _pollenToHoneyRate;
            _honeyStock++;
        }

        TrySpawnBee();
    }

    private void TrySpawnBee()
    {
        while (_honeyStock >= _honeyToBeeRate)
        {
            _honeyStock -= _honeyToBeeRate;
            SpawnBee();
        }
    }

    private void SpawnBee()
    {
        if (_beePrefab != null && _spawnPoint != null)
        {
            GameObject beeObj = Instantiate(_beePrefab, _spawnPoint.position, Quaternion.identity);
            Bee bee = beeObj.GetComponent<Bee>();
            bee.Init(this);
        }
    }

    public int GetPollenStock() => _pollenStock;
    public int GetHoneyStock() => _honeyStock;
}