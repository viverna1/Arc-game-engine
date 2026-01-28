using Engine.Components;
using Engine.Core;
using SFML.System;
using System;

public static class GameScene
{
    public static void Setup(Scene scene)
    {
        // Камера
        var camera = new GameObject("MainCamera");
        var cameraComp = camera.AddComponent<Camera>();
        cameraComp.SetPosition(new Vector2f(0, 0));
        var cameraControlComp = camera.AddComponent<CameraControl>();
        cameraControlComp.camera = cameraComp;
        scene.AddGameObject(camera);
        
        // Игрок
        var player = new GameObject("Player");
        player.AddComponent<Transform>();
        player.AddComponent<SpriteRenderer>().SetOrigin(25, 25);
        player.AddComponent<Player>();
        cameraControlComp!.player = player;

        // Трава
        Random random = new Random();
        for (int i = 0; i < 20; i++)
        {
            Vector2f pos = new Vector2f(
                random.Next(-500, 500),
                random.Next(-500, 500)
            );
            var grass = new GameObject($"Grass_{i}");
            var grassTransform = grass.AddComponent<Transform>();
            grassTransform.Position = pos;
            grassTransform.Rotation = random.Next(0, 360);
            grassTransform.Size = new Vector2f(random.Next(20, 30), random.Next(20, 30));
            grass.AddComponent<SpriteRenderer>().FillColor = new SFML.Graphics.Color(0, 255, 0);
            scene.AddGameObject(grass);
        }

        // Луч
        var ray = new GameObject("Ray");
        var rayTrans = ray.AddComponent<Transform>();
        rayTrans.Size = new Vector2f(100, 5);
        rayTrans.Rotation = 45;
        ray.AddComponent<SpriteRenderer>().FillColor = new SFML.Graphics.Color(255, 50, 50, 128);
        ray.SetActive(false);
        ray.SetParent(player);
        player.GetComponent<Player>()!.RayObject = ray;
        scene.AddGameObject(ray);

        scene.AddGameObject(player);
    }
}