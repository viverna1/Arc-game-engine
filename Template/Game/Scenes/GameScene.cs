using Arc.Components;
using Arc.Core;
using SFML.System;

public static class GameScene
{
    public static void Setup(Scene scene)
    {
        // Камера
        GameObject camera = new("Camera");
        camera.AddComponent<Camera>();
        scene.AddGameObject(camera);

        // Объект
        GameObject obj = new("object");
        obj.AddComponent<Transform>();
        obj.AddComponent<SpriteRenderer>();
        scene.AddGameObject(obj);
    }
}