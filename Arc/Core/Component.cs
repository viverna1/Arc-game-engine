using Arc.Components;

namespace Arc.Core;

public abstract class Component
{
    public GameObject gameObject { get; set; } = null!;
    
    public virtual void Start() { }
    public virtual void Update(float deltaTime) { }

    public Transform transform => gameObject?.transform;
}
