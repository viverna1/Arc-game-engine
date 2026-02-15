using Arc.Components;
using Arc.System;
using SFML.Graphics;
using System.Collections.Generic;
using System.Linq;

namespace Arc.Core;

public class Scene
{
    public Color BackgroundColor = new(0x0f, 0x12, 0x2a);

    private static Scene _instance;
    public static Scene Instance => _instance ??= new Scene();
    
    private List<GameObject> _gameObjects = [];
    
    public void AddGameObject(GameObject gameObject)
    {
        _gameObjects.Add(gameObject);
        gameObject.Start();
    }
    
    public void RemoveGameObject(GameObject gameObject)
    {
        _gameObjects.Remove(gameObject);
    }
    
    public GameObject Find(string name) => _gameObjects.Find(obj => obj.Name == name);
    
    public List<GameObject> FindWithTag(string tag) => _gameObjects.FindAll(obj => obj.Tag == tag);
    
    public void Update(float deltaTime)
    {
        foreach (var obj in _gameObjects.ToList())
        {
            if (obj.IsActive)
                obj.Update(deltaTime);
        }
    }
    
    public void Render(RenderWindow window)
    {
        Window.Clear(BackgroundColor);

        var renderers = _gameObjects
            .Where(obj => obj.IsActive)
            .Select(obj => obj.GetComponent<IRenderable>())
            .Where(renderer => renderer != null)
            .OrderBy(renderer => renderer.ZLayer);
        
        // 1. Рисуем игровые объекты (не UI) с основной камерой
        window.SetView(Camera.View);
        foreach (var renderer in renderers)
        {
            if (!renderer.IsUI)
                renderer.Draw(window);
        }

        // 2. Рисуем UI с фиксированной View
        window.SetView(Window.UIView);
        foreach (var renderer in renderers)
        {
            if (renderer.IsUI)
                renderer.Draw(window);
        }
    }
}