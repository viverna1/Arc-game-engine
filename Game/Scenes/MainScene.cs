using Engine.Components;
using Engine.Core;
using SFML.System;

public static class GameScene
{
    public static void Setup(Scene scene)
    {
        // Камера
        var camera = new GameObject("MainCamera");
        camera.AddComponent<Camera>();
        scene.AddGameObject(camera);

        // Сетка тайлов
        var tileMapObject = new GameObject("TileMap");
        var tileMap = tileMapObject.AddComponent<TileMap>();
        tileMap.scene = scene;

        // Змейка
        var snake = new GameObject("Snake");
        var snakeComp = snake.AddComponent<Snake>();
        snake.AddComponent<Transform>();
        snake.AddComponent<SpriteRenderer>().FillColor = SFML.Graphics.Color.Green;
        snakeComp.tileMap = tileMap;
    }
}