using UnityEngine;

public class Flower : MonoBehaviour
{
    [SerializeField] private FlowerData _data;
    [SerializeField] private EnvironmentManager _environment;

    private float _growthProgress;

    private float _timer;
    private float _elapsedLifeTime;

    private bool _isGrowing = false;
    private bool _canBePollinated = false;
    private bool _isGrowed = false;

    private Transform _model;

    private void Start()
    {
        if (_data._flowerPrefab != null)
        {
            _model = Instantiate(_data._flowerPrefab, transform).transform;
            _model.localScale = Vector3.zero;
        }
    }

    private void Update()
    {
        if (_isGrowed)
        {
            PassingLife();
            return;
        }

        if (!CanGrow())
        {
            _isGrowing = false;
            return;
        }

        Grow();

    }

    private void PassingLife()
    {
        _elapsedLifeTime += Time.deltaTime;

        Debug.Log(_elapsedLifeTime);

        if (_elapsedLifeTime >= _data._lifeDuration)
        {
            Destroy(gameObject);
        }
    }

    private bool CanGrow()
    {
        return _environment._humidity >= _data._minHumidity && _environment._humidity <= _data._maxHumidity &&
               _environment._sunlight >= _data._minSunlight && _environment._sunlight <= _data._maxSunlight;
    }

    private void Grow()
    {
        _isGrowing = true;
        _timer += Time.deltaTime;
        float flowerProgress = Mathf.Clamp01(_timer / _data._growthDuration);
        _growthProgress = _data._growthCurve.Evaluate(flowerProgress);

        if (_model != null)
            _model.localScale = Vector3.one * _growthProgress;

        if (_growthProgress >= 1)
        {
            _isGrowed = true;
            _canBePollinated = true;
        }
    }

    public bool ContainsPollen() => _canBePollinated;
    public int GetPollen() => _data._pollenAmount;
}