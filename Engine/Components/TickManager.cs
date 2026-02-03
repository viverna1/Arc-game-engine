using System;
using System.Collections.Generic;

namespace Engine.Core;

public class TickManager : Component
{
    private float _tickRate = 20f;
    private float _timer;
    
    private List<ITickable> _components = new();
    
    public override void Update(float deltaTime)
    {
        _timer += deltaTime;
        float tickInterval = 1f / _tickRate;
        
        if (_timer >= tickInterval)
        {
            foreach (var tickable in _components.ToArray())
            {
                tickable.OnTick();
            }
            _timer -= tickInterval;
        }
    }

    public void AddComponent(ITickable comp)
    {
        _components.Add(comp);
    }

    public void RemoveComponent(ITickable comp)
    {
        _components.Remove(comp);
    }

    public void SetTickRate(float newRate)
    {
        _timer = 0;
        _tickRate = newRate;
    }
}