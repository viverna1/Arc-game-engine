using Arc;
using Arc.Core;
using Arc.System;

class GameApp
{
    static void Main()
    {
        Application.Initialize(1280, 720, "My Game", "icon.png");
        Window.SetFramerateLimit(60);
        
        GameScene.Setup(Scene.Instance);
        
        Application.Instance.Run();
    }
}