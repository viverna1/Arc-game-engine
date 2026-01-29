using Engine.Components;
using SFML.Graphics;
using System.Collections.Generic;
using System.Linq;

namespace Engine.Core;

public class Scene
{
    public string Name { get; }
    private List<GameObject> _gameObjects = new();
    
    public Scene(string name = "Scene")
    {
        Name = name;
    }
    
    public void AddGameObject(GameObject gameObject)
    {
        _gameObjects.Add(gameObject);
    }
    
    public void RemoveGameObject(GameObject gameObject)
    {
        _gameObjects.Remove(gameObject);
    }

    public GameObject? Find(string name) => _gameObjects.Find(obj => obj.Name == name);
    
    public List<GameObject> FindWithTag(string tag) => _gameObjects.FindAll(obj => obj.Tag == tag);
    
    public void Start()
    {
        foreach (var obj in _gameObjects)
            obj.Start();
    }
    
    public void Update(float deltaTime)
    {
        // foreach (var obj in _gameObjects)
        //     obj.Update(deltaTime);

        var objects = _gameObjects.ToList();
    
        foreach (var obj in objects)
        {
            if (obj.IsActive)
                obj.Update(deltaTime);
        }
    }
    
    public void Render(RenderWindow window)
    {
        // Сортировка по ZLayer
        var sorted = _gameObjects
            .Where(obj => obj.IsActive)
            .OrderBy(obj => obj.GetComponent<SpriteRenderer>()?.ZLayer ?? 0);
        
        foreach (var obj in sorted)
        {
            obj.GetComponent<SpriteRenderer>()?.Draw(window);
        }
    }
}