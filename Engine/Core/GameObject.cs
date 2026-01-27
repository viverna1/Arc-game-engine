using System.Collections.Generic;
using System.Linq;

namespace Engine.Core;

public class GameObject
{
    public string Name { get; set; } = "Game Object";
    public string TagString { get; set; } = "Untagged";
    public bool IsActive { get; set; } = true;

    private List<Component> _components = [];

    public GameObject(string? name = null)
    {
        if (name != null)
            Name = name;
    }

    public T AddComponent<T>() where T : Component, new()
    {
        T component = new();
        component.GameObject = this;
        _components.Add(component);
        component.Start();
        return component;
    }

    public T? GetComponent<T>() where T : Component
    {
        return _components.OfType<T>().FirstOrDefault();
    }

    public void Start()
    {
        foreach (var component in _components)
        {
            component.Start();
        }
    }

    public void Update(float deltaTime)
    {
        foreach (var component in _components)
        {
            component.Update(deltaTime);
        }
    }

    public Components.Transform? Transform => GetComponent<Components.Transform>();
}
