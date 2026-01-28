using Engine.Components;

namespace Engine.Core;

public abstract class Component
{
    public GameObject gameObject { get; set; } = null!;
    public float TickRate { get; set; } = 20f;
    private float _timer;
    
    public virtual void Start() { }
    public virtual void OnTick() { }
    public virtual void Update(float deltaTime)
    {
        _timer += deltaTime;
        float tickInterval = 1 / TickRate;
        if (_timer >= tickInterval)
        {
            OnTick();
            _timer -= tickInterval;
        }
    }

    public Transform? transform => gameObject?.transform;
}
