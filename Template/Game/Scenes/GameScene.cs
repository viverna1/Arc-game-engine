using Arc.Components;
using Arc.Components.UI;
using Arc.Core;
using SFML.System;
using System;

public static class GameScene
{
    public static void Setup(Scene scene)
    {
        GameObject tickManagerObj = new("Object");
        var tickManager = tickManagerObj.AddComponent<TickManager>();
        scene.AddGameObject(tickManagerObj);
        
        // Создаем Canvas с Transform
        GameObject canvas = new("Canvas");
        
        var tileMapObj = new GameObject("TileMap");
        var tileMap = tileMapObj.AddComponent<TileMap>();
        scene.AddGameObject(tileMapObj);
        
        var indicatorObj = new GameObject("Indicator");
        var indicatorRect = indicatorObj.AddComponent<RectTransform>();
        indicatorRect.Size = new Vector2f(200, 50);
        var indicatorImage = indicatorObj.AddComponent<Image>();
        indicatorImage.FillColor = new SFML.Graphics.Color(100, 255, 100);
        indicatorObj.SetParent(canvas); // Важно: родитель - Canvas!
        scene.AddGameObject(indicatorObj);
        
        var tileMapEditorObj = new GameObject("TileMapEditor");
        var tileMapEditor = tileMapEditorObj.AddComponent<TileMapEditor>();
        tileMapEditor.tileMap = tileMap;
        tileMapEditor.indicatorImage = indicatorImage;
        tileMapEditor.tickManager = tickManager;
        scene.AddGameObject(tileMapEditorObj);
        
        var cam = new GameObject("CameraControl");
        cam.AddComponent<CameraControl>();
        scene.AddGameObject(cam);

        scene.AddGameObject(canvas);
    }
}