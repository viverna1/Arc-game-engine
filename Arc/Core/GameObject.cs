using System.Collections.Generic;
using System.Linq;

namespace Arc.Core;

public class GameObject
{
    public string Name { get; set; } = "Game Object";
    public string Tag { get; set; } = "Untagged";
    public bool IsActive { get; set; } = true;
    
    private List<Component> _components = [];
    
    // Иерархия
    private GameObject? _parent;
    private List<GameObject> _children = [];
    
    public GameObject? Parent => _parent;
    public IReadOnlyList<GameObject> Children => _children;

    public GameObject(string? name = null)
    {
        if (name != null)
            Name = name;
    }

    // Управление иерархией
    public void SetParent(GameObject? parent)
    {
        // Удаляем из старого родителя
        _parent?._children.Remove(this);
        
        // Добавляем к новому
        _parent = parent;
        _parent?._children.Add(this);
    }

    public void AddChild(GameObject child)
    {
        child.SetParent(this);
    }

    public void RemoveChild(GameObject child)
    {
        if (_children.Remove(child))
        {
            child._parent = null;
        }
    }

    public T AddComponent<T>() where T : Component, new()
    {
        T component = new();
        component.gameObject = this;
        _components.Add(component);
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
        
        foreach (var child in _children)
        {
            child.Start();
        }
    }

    public void Update(float deltaTime)
    {
        if (!IsActive) return;
        
        foreach (var component in _components)
        {
            component.Update(deltaTime);
        }
        
        var children = _children.ToList();
        foreach (var child in children)
        {
            child.Update(deltaTime);
        }
    }

    public void SetActive(bool state) => IsActive = state;
    
    public Components.Transform? transform => GetComponent<Components.Transform>();
}
