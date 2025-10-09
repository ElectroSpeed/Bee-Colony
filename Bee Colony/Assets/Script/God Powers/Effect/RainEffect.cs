using System.Collections.Generic;

public class RainEffect : IEffect
{
    private EnvironmentManager _env;
    private float _intensity;

    public RainEffect(EnvironmentManager env, float intensity)
    {
        _env = env;
        _intensity = intensity;
    }

    public void ApplyEffect(IEnumerable<ITarget> targets)
    {
        _env.SetHumidity(_env._humidity + _intensity);
    }
}