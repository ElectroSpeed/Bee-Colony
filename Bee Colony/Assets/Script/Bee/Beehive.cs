using System;
using System.Collections.Generic;
using UnityEngine;

public class Beehive : MonoBehaviour, IPollenReceiver
{
    [Header("Stock")]
    [SerializeField] private int _pollenStock = 0;
    [SerializeField] private int _honeyStock = 0;

    [Header("Production Rates")]
    [SerializeField] private int _pollenToHoneyRate = 5;
    [SerializeField] private int _honeyToBeeRate = 3;

    [Header("Bee Spawning")]
    [SerializeField] private GameObject _beePrefab;
    [SerializeField] private Transform _spawnPoint;

    private readonly List<Bee> _activeBees = new();
    
    public event Action<int> OnPollenChanged;
    public event Action<int> OnHoneyChanged;
    public event Action<int> OnBeeCountChanged;

    private void Awake()
    {
        foreach (Bee bee in FindObjectsByType<Bee>(FindObjectsSortMode.None))
        {
            bee.Init(this);
            RegisterBee(bee);
        }
        
        OnPollenChanged?.Invoke(_pollenStock);
        OnHoneyChanged?.Invoke(_honeyStock);
        OnBeeCountChanged?.Invoke(_activeBees.Count);
    }

    public void CheckBeeCount()
    {
        foreach (Bee bee in FindObjectsByType<Bee>(FindObjectsSortMode.None))
        {
            if (_activeBees.Contains(bee) == false)
            {
                bee.Init(this);
                RegisterBee(bee);
            }
        }
        OnBeeCountChanged?.Invoke(_activeBees.Count);
    }

    public void AddPollen(int amount)
    {
        _pollenStock += amount;
        OnPollenChanged?.Invoke(_pollenStock);
        TryProduceHoney();
    }

    private void TryProduceHoney()
    {
        bool honeyProduced = false;

        while (_pollenStock >= _pollenToHoneyRate)
        {
            _pollenStock -= _pollenToHoneyRate;
            _honeyStock++;
            honeyProduced = true;
        }

        if (honeyProduced)
        {
            OnPollenChanged?.Invoke(_pollenStock);
            OnHoneyChanged?.Invoke(_honeyStock);
        }

        TrySpawnBee();
    }

    private void TrySpawnBee()
    {
        bool beeSpawned = false;

        while (_honeyStock >= _honeyToBeeRate)
        {
            _honeyStock -= _honeyToBeeRate;
            SpawnBee();
            beeSpawned = true;
        }

        if (beeSpawned)
            OnHoneyChanged?.Invoke(_honeyStock);
    }

    private void SpawnBee()
    {
        if (_beePrefab != null && _spawnPoint != null)
        {
            GameObject beeObj = Instantiate(_beePrefab, _spawnPoint.position, Quaternion.identity);
            Bee bee = beeObj.GetComponent<Bee>();
            bee.Init(this);
            RegisterBee(bee);
        }
    }
    
    public void RegisterBee(Bee bee)
    {
        if (!_activeBees.Contains(bee))
        {
            _activeBees.Add(bee);
            OnBeeCountChanged?.Invoke(_activeBees.Count);
        }
    }

    public void UnregisterBee(Bee bee)
    {
        if (_activeBees.Remove(bee))
            OnBeeCountChanged?.Invoke(_activeBees.Count);
    }
    
    public int GetBeeCount() => _activeBees.Count;
    public int GetPollenStock() => _pollenStock;
    public int GetHoneyStock() => _honeyStock;
}