using SFML.Graphics;
using System.Collections.Generic;
using System.Linq;

namespace Arc.Core;

public class Scene
{
    private static Scene _instance;
    public static Scene Instance => _instance ??= new Scene();
    
    private List<GameObject> _gameObjects = [];
    private List<Renderer> _cachedRenderers = [];
    private bool _renderersCacheDirty = true;
    
    public void AddGameObject(GameObject gameObject)
    {
        _gameObjects.Add(gameObject);
        _renderersCacheDirty = true;
        gameObject.Start();
    }
    
    public void RemoveGameObject(GameObject gameObject)
    {
        _gameObjects.Remove(gameObject);
        _renderersCacheDirty = true;
    }
    
    private void RebuildRenderersCache()
    {
        _cachedRenderers.Clear();
        
        foreach (var obj in _gameObjects)
        {
            var renderer = obj.GetComponent<Renderer>();
            if (renderer != null)
            {
                _cachedRenderers.Add(renderer);
            }
        }
        
        // Сортируем один раз
        _cachedRenderers.Sort((a, b) => a.ZLayer.CompareTo(b.ZLayer));
        _renderersCacheDirty = false;
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
    
    // Если ZLayer может меняться в рантайме
    private void SortRenderersIfNeeded()
    {
        // Проверяем, изменился ли хотя бы один ZLayer
        bool needsResort = false;
        for (int i = 1; i < _cachedRenderers.Count; i++)
        {
            if (_cachedRenderers[i].ZLayer < _cachedRenderers[i - 1].ZLayer)
            {
                needsResort = true;
                break;
            }
        }
        
        if (needsResort)
        {
            _cachedRenderers.Sort((a, b) => a.ZLayer.CompareTo(b.ZLayer));
        }
    }
    
    public void Render(RenderWindow window)
    {
        if (_renderersCacheDirty)
        {
            RebuildRenderersCache();
        }
        else
        {
            SortRenderersIfNeeded(); // только если кеш не пересобирался
        }
        
        foreach (var renderer in _cachedRenderers)
        {
            if (renderer.gameObject.IsActive)
            {
                renderer.Draw(window);
            }
        }
    }
}