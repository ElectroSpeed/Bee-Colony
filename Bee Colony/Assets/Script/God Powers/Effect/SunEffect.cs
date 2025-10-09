using System.Collections.Generic;

public class SunEffect : IEffect
{
    private EnvironmentManager _env;
    private float _intensity;

    public SunEffect(EnvironmentManager env, float intensity)
    {
        _env = env;
        _intensity = intensity;
    }

    public void ApplyEffect(IEnumerable<ITarget> targets)
    {
        _env.SetSunlight(_env._sunlight + _intensity);
    }
}