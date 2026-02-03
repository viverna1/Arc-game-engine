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

        // Тик менеджер
        var tickManager = new GameObject("TickManager");
        var tickManagerComp = tickManager.AddComponent<TickManager>();
        tickManagerComp.SetTickRate(10);
        scene.AddGameObject(tickManager);

        // Сетка тайлов
        var tileMapObject = new GameObject("TileMap");
        var tileMap = tileMapObject.AddComponent<TileMap>();
        scene.AddGameObject(tileMapObject);

        // Змейка
        var snake = new GameObject("Snake");
        var snakeComp = snake.AddComponent<Snake>();
        snake.AddComponent<Transform>();
        snakeComp.tileMap = tileMap;
        scene.AddGameObject(snake);

        tickManagerComp.AddComponent(snakeComp);
    }
}