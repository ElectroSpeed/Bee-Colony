using UnityEngine;

[CreateAssetMenu(menuName = "GodGame/PowerData")]
public class PowerData : ScriptableObject
{
    [SerializeField] private string _powerName;
    [SerializeField] private float _cooldown = 2f;

    [SerializeField] private TargetingBehaviour _targetingPrefab;
    [SerializeField] private EffectBehaviour[] _effectPrefabs;
    
    [SerializeField] private SoundType _soundEffect;

    public string PowerName => _powerName;
    public float Cooldown => _cooldown;
    public TargetingBehaviour TargetingPrefab => _targetingPrefab;
    public EffectBehaviour[] EffectPrefabs => _effectPrefabs;
    public SoundType SoundEffect => _soundEffect;
}