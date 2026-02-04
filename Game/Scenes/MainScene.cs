using Arc.Components;
using Arc.Core;
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

        tileMap.Width = 20;
        tileMap.Height = 11;
        
        tileMap.Width = 40;
        tileMap.Height = 22;
        tileMap.TileSize = 32;
        
        tileMap.Width = 80;
        tileMap.Height = 44;
        tileMap.TileSize = 16;
        scene.AddGameObject(tileMapObject);

        // Змейка
        // var snake = new GameObject("Snake");
        // var snakeComp = snake.AddComponent<Snake>();
        // snake.AddComponent<Transform>();
        // snakeComp.tileMap = tileMap;
        // tickManagerComp.AddComponent(snakeComp);
        // scene.AddGameObject(snake);

        // Игра жизнь
        var gameOfLife = new GameObject("GameOfLife");
        var gameOfLifeComp = gameOfLife.AddComponent<GameOfLife>();
        gameOfLifeComp.tileMap = tileMap;
        tickManagerComp.AddComponent(gameOfLifeComp);
        scene.AddGameObject(gameOfLife);
    }
}