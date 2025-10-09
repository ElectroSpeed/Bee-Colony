using UnityEngine;
using System;

public class EnvironmentManager : MonoBehaviour
{
    [Range(0, 100)] public float _sunlight;
    [Range(0, 100)] public float _humidity;

    public event Action<float> OnSunlightChanged;
    public event Action<float> OnHumidityChanged;

    public void SetSunlight(float value)
    {
        _sunlight = Mathf.Clamp(value, 0f, 100f);
        OnSunlightChanged?.Invoke(_sunlight);
    }

    public void SetHumidity(float value)
    {
        _humidity = Mathf.Clamp(value, 0f, 100f);
        OnHumidityChanged?.Invoke(_humidity);
    }
}